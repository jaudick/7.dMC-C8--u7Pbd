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

    public CheckPoint[] checkPoints;
    public int currentCheckpointNum = 0;
    public static bool destroyProjectiles = false;
    public bool didCheckpointChange = false;

    private void Awake()
    {
        instance = this;
        respawning = false;
        didCheckpointChange = false;
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
        destroyProjectiles = true;
        float counter = 0;
        Vector3 originalPosition = player.transform.position;
        source.PlayOneShot(respawnSound, 0.7f);
        while (counter < 0.5f)
        {
            player.transform.position = originalPosition;
            counter += Time.deltaTime;
            color.postExposure.value += Time.deltaTime * 10;
            yield return new WaitForEndOfFrame();
            destroyProjectiles = false;
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

    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.RightBracket) || Input.GetAxis("CheckpointController") > 0) && !respawning)
        {
            didCheckpointChange = true;
            currentCheckpointNum += currentCheckpointNum >= checkPoints.Length - 1 ? 0 : 1;
            lastCheckpoint = checkPoints[currentCheckpointNum];
            StartCoroutine(RespawnCo());
        }

        else if((Input.GetKeyDown(KeyCode.LeftBracket) || Input.GetAxis("CheckpointController") < 0) && !respawning)
        {
            didCheckpointChange = true;
            currentCheckpointNum -= currentCheckpointNum <= 0 ? 0 : 1;
            lastCheckpoint = checkPoints[currentCheckpointNum];
            StartCoroutine(RespawnCo());
        }
    }
}
