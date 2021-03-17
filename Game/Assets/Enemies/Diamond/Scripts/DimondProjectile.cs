using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimondProjectile : Controller
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
        setTime(TimeCore.times[GetComponent<Shiftable>().timeZone]);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")&&!frozen)
        {
            DestroyParticle();
            Destroy(gameObject);
            //Debug.Log("<color=red>Dead</color>");
        }
        else if(collision.gameObject!=parent && !frozen)
        {
            DestroyParticle();
            Destroy(gameObject);
        }

    }

    public override void setTime(float f)
    {
        localTime = f;
        frozen = f == 0;
        if (frozen)
        {
            gameObject.layer = 8;
            projectileAudio.PlayInactive();
        }
        else
        {
            gameObject.layer = 9;
            projectileAudio.PlayActive();
        }
    }

    private void Update()
    {
        if (!frozen)
        {
            transform.Rotate(0, 0, 200 * Time.deltaTime * localTime);
            rbody.isKinematic = false;
            rbody.velocity = velocity * localTime;
            timeTillDestroy += Time.deltaTime * localTime;
            if (timeTillDestroy >= timeToDestroy)
            {
                DestroyParticle();
            }
        }
        else
        {
            rbody.velocity = Vector3.zero;
            rbody.isKinematic = true;
        }
    }

    private void DestroyParticle()
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
        Destroy(gameObject);
    }
}
