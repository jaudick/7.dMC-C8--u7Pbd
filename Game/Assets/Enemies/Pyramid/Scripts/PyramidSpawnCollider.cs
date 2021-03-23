using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidSpawnCollider : MonoBehaviour
{
    [SerializeField] private SpawnedPyramid spawnedPyramid;
    Rigidbody rbody;
    [SerializeField] private GameObject trailPartice;
    [SerializeField] private GameObject hitParticle;
    public ParticleHolder particleHolder;

    private void Awake()
    {
        particleHolder = FindObjectOfType<ParticleHolder>();
    }
    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rbody.velocity = spawnedPyramid.GetComponent<Rigidbody>().velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !spawnedPyramid.frozen)
        {
            StartCoroutine(DestroyParticle());
        }

        else if (collision.gameObject != spawnedPyramid.parent && !spawnedPyramid.frozen)
        {
            StartCoroutine(DestroyParticle());
        }

    }

    public IEnumerator DestroyParticle()
    {
        if (trailPartice != null)
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
            AudioSource audio = GetComponentInParent<AudioSource>();

            while (audio.volume > 0)
            {
                audio.volume -= 0.1f;
                yield return new WaitForEndOfFrame();
            }
            audio.Stop();
            Destroy(transform.parent.gameObject);
        }
    }

    public void VoidDestroyParticle()
    {
        StartCoroutine(DestroyParticle());
    }
}
