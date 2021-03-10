using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private PlayerMovementRigidbody player;
    public CheckPoint lastCheckpoint;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovementRigidbody>();
    }

    public void Respawn()
    {
        player.transform.position = lastCheckpoint.transform.position;
    }
}
