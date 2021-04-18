using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;
    private PlayerMovementRigidbody player;
    public CheckPoint lastCheckpoint;
    private Volume volume;
    private ColorAdjustments color;
    private AudioSource source;
    public AudioClip respawnSound;
    public bool isInvincible;
    private bool respawning;

    private void Awake()
    {
        instance = this;
        respawning = false;
        source = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerMovementRigidbody>();
        volume = FindObjectOfType<Volume>();
        if (volume.profile.TryGet<ColorAdjustments>(out var colors))
             color = colors;
    }

    
    public void Respawn()
    {
        if(!isInvincible && !respawning)
            StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        respawning = true;
        float counter = 0;
        Vector3 originalPosition = player.transform.position;
        source.PlayOneShot(respawnSound, 0.7f);
        while (counter < 0.5f)
        {
            player.transform.position = originalPosition;
            counter += Time.deltaTime;
            color.postExposure.value += Time.deltaTime * 10;
            yield return new WaitForEndOfFrame();
        }

        counter = 0;
        while (counter < 0.25f)
        {
            if (counter < 0.07f)
            {
                player.transform.position = lastCheckpoint.transform.position;
                player.transform.rotation = lastCheckpoint.transform.rotation;
            }
            counter += Time.deltaTime;
            color.postExposure.value -= Time.deltaTime * 20f;
            yield return new WaitForEndOfFrame();
        }
        color.postExposure.value = 0;
        respawning = false;
    }
}
