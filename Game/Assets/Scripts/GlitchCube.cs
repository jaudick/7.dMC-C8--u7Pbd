using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchCube : MonoBehaviour
{
    public GameObject cube;
    public Vector3 scale;
    public bool rotationGlitch = true;

    void Start()
    {
        scale = transform.localScale;
        StartCoroutine(GlitchCubeMethod());
    }

    IEnumerator GlitchCubeMethod()
    {
        //cube.SetActive(false);
        transform.localScale = new Vector3(scale.x * Random.Range(0.25f, 1), scale.y * Random.Range(0.25f, 1), scale.z * Random.Range(0.25f, 1));
        if (rotationGlitch)
        {
            transform.eulerAngles = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        }
        yield return new WaitForSeconds(Random.Range(0.1f, 0.75f));
        //cube.SetActive(true);
        //yield return new WaitForSeconds(Random.Range(0.1f, 0.75f));
        StartCoroutine(GlitchCubeMethod());
    }
}
