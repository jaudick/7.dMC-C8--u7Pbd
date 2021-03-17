using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ForceField : MonoBehaviour
{
    private Volume volume;
    ColorAdjustments colors;
    private bool colorsOn;

    private void Awake()
    {
        volume = FindObjectOfType<Volume>();
        if (volume.profile.TryGet<ColorAdjustments>(out var postColors))
            colors = postColors;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null)
        {
            colorsOn = false;
            StopCoroutine(Colors());
            StartCoroutine(BlackAndWhite());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovementRigidbody>() != null)
        {
            colorsOn = true;         
            StopCoroutine(BlackAndWhite());
            StartCoroutine(Colors());
        }
    }


    IEnumerator BlackAndWhite()
    {
        while (colors.saturation.value > -100f && !colorsOn)
        {
            colors.saturation.value -= Time.deltaTime * 200;
            yield return new WaitForEndOfFrame();
            if (colors.saturation.value <= -100f)
            {
                colors.saturation.value = -100f;
                break;
            }
        }


    }

    IEnumerator Colors()
    {
        while (colors.saturation.value < 0f && colorsOn)
        {
            colors.saturation.value += Time.deltaTime * 10;
            yield return new WaitForEndOfFrame();
            if (colors.saturation.value >= 0f)
            {
                colors.saturation.value = 0f;
                break;
            }
        }
    }
}
