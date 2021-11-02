using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D20Projectile : Controller
{
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject trailPartice;
    public ParticleHolder particleHolder;
    //[SerializeField] private Shiftable shiftable;
    public GameObject parent;
    private Vector3 velocity;
    private Rigidbody rbody;
    private float localTime;
    private float timeTillDestroy = 0;
    private float timeToDestroy = 3f;
    private bool frozen = false;
    [SerializeField] private ProjectileAudio projectileAudio;
    private void Awake()
    {
        //shiftable = GetComponent<Shiftable>();
    }
    void Start()
    {
        particleHolder = FindObjectOfType<ParticleHolder>();
        rbody = GetComponent<Rigidbody>();
        velocity = rbody.velocity;
        setTime(GetComponent<Shiftable>().timeZone == -1 ? 1 : TimeCore.times[GetComponent<Shiftable>().timeZone]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !frozen)
        {
            StartCoroutine(DestroyParticle());
            //Debug.Log("<color=red>Dead</color>");
        }
        else if (collision.gameObject != parent && !frozen)
        {
            StartCoroutine(DestroyParticle());
            //Debug.Log("<color=yellow>Destroyed</color>");
        }

    }

    public override void setTime(float f)
    {
        localTime = f;
        frozen = f == 0;
        if (frozen)
        {
            projectileAudio.PlayInactive();
            gameObject.layer = 8;
        }
        else
        {
            projectileAudio.PlayActive();
            gameObject.layer = 9;
        }
    }

    private void Update()
    {
        if (!frozen)
        {
            rbody.isKinematic = false;
            rbody.velocity = velocity * localTime;
            timeTillDestroy += Time.deltaTime * localTime;
            if (timeTillDestroy >= timeToDestroy)
            {
                StartCoroutine(DestroyParticle());
            }
        }
        else
        {
            rbody.velocity = Vector3.zero;
            rbody.isKinematic = true;
        }
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
            audio.volume -= 0.1f;
            yield return new WaitForEndOfFrame();
        }
        audio.Stop();
        Destroy(gameObject);
    }
}
