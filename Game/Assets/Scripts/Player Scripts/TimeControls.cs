using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeControls : MonoBehaviour
{
    private PlayerAudio playerAudio;
    public static bool canShift = true;
    private Volume volume;
    private LensDistortion distortion;
    private ColorAdjustments colors;
    private ChromaticAberration chromatic;
    private int currentTimeZone = 0;
    [SerializeField] private Material[] skyBoxes;
    //private Vignette vignette;

    private float chromaticInit = 0f;
    //private float vignetteInit = 0f;
    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
        //postProcessing = FindObjectOfType<Volume>().GetComponent<Animator>();
        volume = FindObjectOfType<Volume>();

        if (volume.profile.TryGet<LensDistortion>(out var lens))
            distortion = lens;
        if (volume.profile.TryGet<ColorAdjustments>(out var postColors))
            colors = postColors;
        if (volume.profile.TryGet<ChromaticAberration>(out var chrom))
        {
            chromatic = chrom;
            chromaticInit = chrom.intensity.value;
        }

        /*
        if (volume.profile.TryGet<Vignette>(out var vin))
        {
            vignette = vin;
            vignetteInit = vin.intensity.value;
        }*/
    }
    void Update()
    {
        if (canShift && !GameCanvas.paused)
        {
            if ((Input.GetButtonDown("PinkController") || Input.GetButtonDown("Time1") || Input.GetKeyDown(KeyBindingManager.instance.TIME1)) && currentTimeZone!=1)
            {
                currentTimeZone = 1;
                playerAudio.PlayWarp(1);
                DebugTime(1);
                TimeCore.Shift(0);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift(0));
            }
            else if ((Input.GetButtonDown("BlueController") || Input.GetButtonDown("Time2") || Input.GetKeyDown(KeyBindingManager.instance.TIME2)) && currentTimeZone != 2)
            {
                currentTimeZone = 2;
                playerAudio.PlayWarp(2);
                DebugTime(2);
                TimeCore.Shift(1);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift(1));
            }
            else if ((Input.GetButtonDown("GreenController") || Input.GetButtonDown("Time3") || Input.GetKeyDown(KeyBindingManager.instance.TIME3)) && currentTimeZone != 3)
            {
                currentTimeZone = 3;
                playerAudio.PlayWarp(3);
                DebugTime(3);
                TimeCore.Shift(2);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift(2));
            }
            else if ((Input.GetButtonDown("OrangeController") || Input.GetButtonDown("Time4") || Input.GetKeyDown(KeyBindingManager.instance.TIME4)) && currentTimeZone != 4)
            {
                currentTimeZone = 4;
                playerAudio.PlayWarp(4);
                DebugTime(4);
                TimeCore.Shift(3);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift(3));
            }

            ///////////////scroll wheel
            else if ((Input.GetAxis("Time3MouseWheel") > 0 && KeyBindingManager.instance.SCROLL_WHEEL) && currentTimeZone != 3)
            {
                RenderSettings.skybox = skyBoxes[2];
                currentTimeZone = 3;
                playerAudio.PlayWarp(3);
                DebugTime(3);
                TimeCore.Shift(2);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift(2));
            }
            else if ((Input.GetAxis("Time4MouseWheel") < 0 && KeyBindingManager.instance.SCROLL_WHEEL) && currentTimeZone != 4)
            {
                currentTimeZone = 4;
                playerAudio.PlayWarp(4);
                DebugTime(4);
                TimeCore.Shift(3);
                StartCoroutine(DistortForTimeShift());
                StartCoroutine(Shift(3));
            }
        }
    }

    IEnumerator DistortForTimeShift()
    {
        while (distortion.intensity.value > -1f)
        {
            distortion.intensity.value -= Time.deltaTime * 6;

            colors.hueShift.value -= Time.deltaTime * 1000;
            colors.hueShift.value = Mathf.Clamp(colors.hueShift.value, -180, 0);

            chromatic.intensity.value += Time.deltaTime * 4;
            chromatic.intensity.value = Mathf.Clamp(chromatic.intensity.value, chromaticInit, 1);

            //vignette.intensity.value += Time.deltaTime;
            //vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, vignetteInit, 1);
            yield return new WaitForEndOfFrame();
        }
        //colors.hueShift.value = -180;
        while (distortion.intensity.value < 0)
        {
            distortion.intensity.value += Time.deltaTime * 4;
            colors.hueShift.value += Time.deltaTime * 1000;
            colors.hueShift.value = Mathf.Clamp(colors.hueShift.value, -180, 0);

            chromatic.intensity.value -= Time.deltaTime * 8;
            chromatic.intensity.value = Mathf.Clamp(chromatic.intensity.value, chromaticInit, 1);

            //vignette.intensity.value -= Time.deltaTime;
            //vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, vignetteInit, 1);
            yield return new WaitForEndOfFrame();
        }

        colors.hueShift.value = 0;
        //vignette.intensity.value = vignetteInit;


    }

    private IEnumerator Shift(int skyBox)
    {
        canShift = false;
        yield return new WaitForSeconds(0.1f);
        RenderSettings.skybox = skyBoxes[skyBox];
        yield return new WaitForSeconds(0.4f);
        canShift = true;
    }

    private void DebugTime(int time)
    {
        Debug.Log("Current Time Zone = <color=cyan>" + time + "</color>");
    }
}
