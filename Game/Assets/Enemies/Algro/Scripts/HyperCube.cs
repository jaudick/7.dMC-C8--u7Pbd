using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperCube : Controller
{
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject trailPartice;
    public ParticleHolder particleHolder;
    public GameObject targ;
    public float burst = 2;
    public float speed = 20;
    private float init;
    public GameObject parent;
    private float localTime;
    private Vector3 last;
    private Vector3 offset;
    private Rigidbody rbody;
    public bool isTracking = true;
    private bool frozen;
    private float timeTillDestroy = 0;
    private float timeToDestroy = 10f;
    [SerializeField] private ProjectileAudio hyperCubeAudio;

    public override void setTime(float f)
    {
        localTime = f;
        frozen = f == 0;
        if (frozen)
        {
            hyperCubeAudio.PlayInactive();
            rbody.isKinematic = true;
            gameObject.layer = 8;
        }
        else
        {
            hyperCubeAudio.PlayActive();
            rbody.isKinematic = false;
            gameObject.layer = 9;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        init = speed;
        particleHolder = FindObjectOfType<ParticleHolder>();
        rbody = GetComponent<Rigidbody>();
        localTime = TimeCore.times[GetComponent<Shiftable>().timeZone];
        setTime(localTime);
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        int tracking = isTracking ? 1 : 0;
        rbody.velocity *= localTime * tracking;
        if (!frozen)
        {
            rbody.isKinematic = false;
            timeTillDestroy += Time.deltaTime * localTime;
            if (timeTillDestroy >= timeToDestroy)
            {
                if (hitParticle != null) Instantiate(hitParticle);
                StartCoroutine(DestroyParticle());
            }
        }
        else
        {
            rbody.velocity = Vector3.zero;
            rbody.isKinematic = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && localTime > 0)
        {
            StartCoroutine(DestroyParticle());
        }
        else if (collision.gameObject != parent && localTime > 0 && collision.gameObject.layer != 9)
        {
            StartCoroutine(DestroyParticle());
        }
    }

    private IEnumerator Spawn()
    {
        speed = init;
        last = targ.transform.position + Vector3.up + offset;
        rbody.velocity = ((Vector3.Normalize(last - transform.position)) * speed) * localTime;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
        StartCoroutine(GetTarget());
    }

    private IEnumerator GetTarget()
    {
        speed = init;
        last = targ.transform.position + Vector3.up + offset/2;
        rbody.velocity = ((Vector3.Normalize(last - transform.position)) * speed) * localTime;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));

        last = targ.transform.position + Vector3.up + offset/2;
        rbody.velocity = ((Vector3.Normalize(last - transform.position)) * speed) * localTime;
        speed = init/2;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

        StartCoroutine(GetTarget());
    }
    private IEnumerator DestroyParticle()
    {
        Vector3 scale = trailPartice.transform.localScale;
        trailPartice.transform.SetParent(particleHolder.transform);
        trailPartice.transform.localScale = scale;
        trailPartice.GetComponent<ParticleDestoyer>().DestroyParticle();
        if (hitParticle != null)
        {
            GameObject explosion = Instantiate(hitParticle, transform.position, Quaternion.identity);
            explosion.GetComponent<ParticleDestoyer>().DestroyParticle(2f);
        }

        AudioSource audio = GetComponent<AudioSource>();

        while (audio.volume > 0)
        {
            audio.volume -= 0.05f;
            yield return new WaitForEndOfFrame();
        }
        audio.Stop();
        Destroy(gameObject);
    }

}
