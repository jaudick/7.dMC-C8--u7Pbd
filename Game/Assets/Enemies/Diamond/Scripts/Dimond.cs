using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimond : Controller
{
    [SerializeField] private PlayerMovementRigidbody player;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootBox;
    private ParticleHolder particleHolder;
    public float bulletSpeed = 10f;
    private Vector3 position1;
    private Vector3 position2;
    private Vector3 predictedPosition;
    private float localTime; 
    private float predicitonAdjusmentIncrease = 4f;
    public float positionPredictionValue = 10f;
    public float distancePredictionValue = 10f;
    private float minimalDistanceToAffectSpeed = 1;
    public float durationBetweenShotsInSeconds = 2f;
    private bool frozen = false;
    private float resetRoation = 5f;
    public bool isTracking;
    [SerializeField] private DimondAudio dimondAudio;
    private void Awake()
    {
        particleHolder = FindObjectOfType<ParticleHolder>();
        player = FindObjectOfType<PlayerMovementRigidbody>();
    }

    private void Start()
    {
        position1 = player.transform.position;
        position2 = player.transform.position;
        predictedPosition = player.transform.position;
        localTime = GetComponent<Shiftable>().timeZone == -1 ? 1 : TimeCore.times[GetComponent<Shiftable>().timeZone];
        setTime(localTime);
        StartCoroutine(GetPositionPrediction());
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen && isTracking)
        {
            //transform.LookAt(player.transform);
            Vector3 whereToLook = player.transform.position - transform.position;
            resetRoation += 2*Time.deltaTime * localTime;
            if (resetRoation >= 6) resetRoation += 50 * Time.deltaTime * localTime;
            resetRoation = Mathf.Clamp(resetRoation, 5, 100);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(whereToLook, Vector3.up), resetRoation * Time.deltaTime * localTime);
        }
    }

    private IEnumerator Shoot()
    {
        float waitTime = Random.Range(0, 1.5f);
        yield return new WaitForSeconds(waitTime);
        if (!frozen && isTracking)
        {
            dimondAudio.PlayDimondSound();
            GameObject spawnedBullet = Instantiate(bullet) as GameObject;
            spawnedBullet.transform.position = shootBox.transform.position;
            Rigidbody bulletBody = spawnedBullet.GetComponent<Rigidbody>();
            spawnedBullet.GetComponent<DimondProjectile>().parent = this.gameObject;

            position2 = player.transform.position;
            predictedPosition = (position2 - position1) * predicitonAdjusmentIncrease;
            float totalChange = Mathf.Abs(predictedPosition.x) + Mathf.Abs(predictedPosition.y) + Mathf.Abs(predictedPosition.z);
            totalChange = Mathf.Clamp(totalChange, 0f, 15f);
            predictedPosition *= totalChange / positionPredictionValue;
            minimalDistanceToAffectSpeed = Vector3.Distance(bulletBody.transform.position, player.transform.position) < 10f ? 2f : 1f; //scales the speed so up close it is still fast and far away it is about the same speed

            spawnedBullet.transform.LookAt(player.transform.position + (predictedPosition / minimalDistanceToAffectSpeed)); //if player is close, adjust look more (* 2), if its far adjust look less (* 1)
            bulletBody.velocity = (spawnedBullet.transform.forward * bulletSpeed * (Vector3.Distance(bulletBody.transform.position, player.transform.position) / distancePredictionValue) * minimalDistanceToAffectSpeed); //it just works

            Shiftable projectileTimeZone = bullet.GetComponent<Shiftable>();
            projectileTimeZone.timeZone = GetComponent<Shiftable>().timeZone;
            spawnedBullet.GetComponent<DimondProjectile>().particleHolder = particleHolder;

            yield return new WaitForSeconds(durationBetweenShotsInSeconds);
            StartCoroutine(Shoot());
        }
        else
        {
            yield return new WaitForSeconds(durationBetweenShotsInSeconds);
            StartCoroutine(Shoot());
        }

    }

    private IEnumerator GetPositionPrediction()
    {
        position1 = player.transform.position;
        yield return new WaitForSeconds(0.275f);
        StartCoroutine(GetPositionPrediction());
    }

    public override void setTime(float f)
    {
        frozen = f == 0;
        localTime = f;
        if (frozen)
        {
            dimondAudio.StopAudio();
            resetRoation = 5;
            gameObject.layer = 8;
        }
        else
        {
            gameObject.layer = 9;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !frozen)
        {
            FindObjectOfType<CheckPointManager>().Respawn();
        }
    }
}
