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

    private void Start()
    {
        particleHolder = FindObjectOfType<ParticleHolder>();
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
            DestroyParticle();
        }

        else if (collision.gameObject != spawnedPyramid.parent && !spawnedPyramid.frozen)
        {
            DestroyParticle();
        }

    }

    public void DestroyParticle()
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
        Destroy(transform.parent.gameObject);
    }
}
