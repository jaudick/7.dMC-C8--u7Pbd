using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyramid : Controller
{
    [SerializeField] private Transform[] spawnBoxes;
    [SerializeField] private PlayerMovementRigidbody player;
    [SerializeField] private GameObject pyramidSpawn;
    [SerializeField] private GameObject triangleCollider;
    private float localTime;
    private bool frozen;
    public float rotationSpeed = 50f;
    public float spawnTime = 2f;
    public float spawnVelocity = 5f;
    [SerializeField] private PyramidAudioManager pyrAudio;

    float timer = 0;

    public override void setTime(float f)
    {
        frozen = f == 0;
        localTime = f;
        if (frozen)
        {
            pyrAudio.StopAudio();
            triangleCollider.layer = 8;
            gameObject.layer = 8;
        }
        else
        {
            triangleCollider.layer = 9;
            gameObject.layer = 9;
        }
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovementRigidbody>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime * localTime);

        timer += Time.deltaTime * localTime;
        if (timer >= spawnTime)
        {
            foreach (Transform transformBox in spawnBoxes)
            {
                GameObject pyr = Instantiate(pyramidSpawn);
                pyr.transform.position = transformBox.position;
                pyr.transform.forward = transformBox.forward;
                Rigidbody rbody = pyr.GetComponent<Rigidbody>();
                rbody.velocity = (pyr.transform.forward.normalized) * spawnVelocity;
                pyr.GetComponent<Shiftable>().timeZone = GetComponent<Shiftable>().timeZone;
                pyr.GetComponent<SpawnedPyramid>().parent = gameObject;
            }
            pyrAudio.PlayPyramidSound();
            timer = 0;
        }
    }

    private void Start()
    {
        localTime = TimeCore.times[GetComponent<Shiftable>().timeZone];
        setTime(localTime);
        //StartCoroutine(Spawn());
    }

    /*
    private IEnumerator Spawn()
    {
        if (!frozen)
        {
            foreach (Transform transformBox in spawnBoxes)
            {
                GameObject pyr = Instantiate(pyramidSpawn);
                pyr.transform.position = transformBox.position;
                pyr.transform.forward = transformBox.forward;
                Rigidbody rbody = pyr.GetComponent<Rigidbody>();
                rbody.velocity = (pyr.transform.forward.normalized) * spawnVelocity;
                pyr.GetComponent<Shiftable>().timeZone = GetComponent<Shiftable>().timeZone;
                pyr.GetComponent<SpawnedPyramid>().parent = gameObject;
            }
            pyrAudio.PlayPyramidSound();
            yield return new WaitForSeconds(spawnTime);
            StartCoroutine(Spawn());
        }

        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(Spawn());
        }
    }*/
}
