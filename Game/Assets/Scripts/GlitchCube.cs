using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchCube : MonoBehaviour
{
    public GameObject cube;
    public Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
        StartCoroutine(GlitchCubeMethod());
    }

    IEnumerator GlitchCubeMethod()
    {
        //cube.SetActive(false);

        float time = Random.Range(0.25f, 1f);
        float timeStarted = Time.time;
        float percentDone = 0;
        Vector3 newScale = new Vector3(scale.x * Random.Range(0.25f, 1), scale.y * Random.Range(0.25f, 1), scale.z * Random.Range(0.25f, 1));
        Vector3 newRotation = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));

        Vector3 currentScale = transform.localScale;
        Vector3 currentRotation = transform.eulerAngles;

        while (percentDone<1)
        {
            percentDone = (Time.time - timeStarted) * time;
            transform.localScale = Vector3.Lerp(currentScale, newScale, percentDone);
            transform.eulerAngles = Vector3.Lerp(currentRotation, newRotation, percentDone);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        StartCoroutine(GlitchCubeMethod());
    }
}
