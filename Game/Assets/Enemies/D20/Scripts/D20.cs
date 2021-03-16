using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20 : Controller
{
    [SerializeField] private PlayerMovementRigidbody player;
    [SerializeField] private GameObject projectile;
    public float bulletSpeed = 10f;
    public float durationBetweenShotsInSeconds = 2f;
    private bool frozen = false;
    private float resetRoation = 5f;
    public bool isTracking = false;
    private float localTime;

    [SerializeField] private Transform[] shootBoxes;
    private D20AudioManager d20Audio;

    private void Awake()
    {
        d20Audio = GetComponent<D20AudioManager>();
        player = FindObjectOfType<PlayerMovementRigidbody>();
    }

    private void Start()
    {
        localTime = TimeCore.times[GetComponent<Shiftable>().timeZone];
        setTime(localTime);
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if (!frozen && isTracking)
        {
            Vector3 whereToLook = player.transform.position - transform.position;
            resetRoation += 2 * Time.deltaTime;
            if (resetRoation >= 6) resetRoation += 50 * Time.deltaTime;
            resetRoation = Mathf.Clamp(resetRoation, 5, 100);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(whereToLook, Vector3.up), resetRoation * Time.deltaTime);
        }
    }

    private IEnumerator Shoot()
    {
        if (!frozen && isTracking)
        {
            d20Audio.PlayD20Sound(1);
            for(int i = 0; i<shootBoxes.Length; i++)
            {
                GameObject bullet = Instantiate(projectile);
                bullet.transform.position = shootBoxes[i].transform.position;
                Rigidbody rbody = bullet.GetComponent<Rigidbody>();
                bullet.transform.LookAt(player.transform);
                rbody.velocity = shootBoxes[i].transform.forward.normalized * bulletSpeed;

                D20Projectile projectileScript = bullet.GetComponent<D20Projectile>();
                projectileScript.parent = gameObject;

                Shiftable projectileTimeZone = bullet.GetComponent<Shiftable>();
                projectileTimeZone.timeZone = GetComponent<Shiftable>().timeZone;
            }
            yield return new WaitForSeconds(durationBetweenShotsInSeconds);
            StartCoroutine(Shoot());
        }
        else
        {
            yield return new WaitForSeconds(durationBetweenShotsInSeconds);
            StartCoroutine(Shoot());
        }

    }

    public override void setTime(float f)
    {
        frozen = f == 0;
        if (frozen)
        {
            resetRoation = 5;
            gameObject.layer = 8;
            d20Audio.StopAudio();
        }
        else
        {
            gameObject.layer = 9;
        }
    }
}
