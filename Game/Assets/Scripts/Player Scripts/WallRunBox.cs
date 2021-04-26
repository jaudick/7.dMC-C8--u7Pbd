using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunBox : MonoBehaviour
{
    private PlayerMovementRigidbody player;
    public bool isRightBox;
    public bool isLeftBox;
    int layer = 1 << 10;
    Collider[] colliders;
    private bool wait = false;
    private bool canTakeNewWall = true;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementRigidbody>();
    }

    /*
    private void Update()
    {
        colliders = Physics.OverlapBox(transform.position, new Vector3(1, 1, 1), Quaternion.identity, layer);
        if ((colliders.Length == 1 && !player.isGrounded && ((player.lastWall == null) || player.lastWall != colliders[0].gameObject))
            && ((KeyBindingManager.instance.HOLD_WALL && (Input.GetKey(KeyBindingManager.instance.JUMP) || (Input.GetAxis("JumpController") > 0)))
            || !KeyBindingManager.instance.HOLD_WALL))
        {
            if (Input.GetAxis("JumpController") > 0)
                player.lastFrameWasHoldingRightTigger = true;
            if (player.getNextWall)
            {
                player.playerAudio.PlayWallRun();
                player.SetLastWalls(colliders[0].gameObject);
                player.getNextWall = false;
                player.rigRotation = transform.rotation;
                player.wallRunVelocity = player.GetVelocity();
            }

            if (colliders[0].GetComponentInParent<MovingHazard>() != null)
            {
                player.currentWallRunUpForce = -10;
            }
            if (isRightBox)
            {
                player.isWallRunning = true;
                player.isWallRunningRight = true;
                player.isWallRunningLeft = false;
            }

            else if (isLeftBox)
            {
                player.isWallRunning = true;
                player.isWallRunningLeft = true;
                player.isWallRunningRight = false;
            }
        }

        else
        {
            player.isWallRunning = false;
            player.isWallRunningLeft = false;
            player.isWallRunningRight = false;
            player.rigRotation = player.transform.rotation;
            player.getNextWall = true;
        }


    }*/

    
    private void OnTriggerStay(Collider other)
    {
        if (!wait && player.canDoInput && (other.CompareTag("WallRun") && !player.isGrounded && ((player.lastWall == null) || player.lastWall!=other.gameObject)) 
            && ((KeyBindingManager.instance.HOLD_WALL && (Input.GetKey(KeyBindingManager.instance.JUMP) || (Input.GetAxis("JumpController") > 0))) 
            || !KeyBindingManager.instance.HOLD_WALL))
        {
            if (Input.GetAxis("JumpController") > 0)
                player.lastFrameWasHoldingRightTigger = true;
            if (player.getNextWall)
            {
                if(other.GetComponentInParent<WallRunCube>()!=null)
                {
                    other.GetComponentInParent<WallRunCube>().SetWallsFalse(other.gameObject);
                }
                player.currentWall = other.gameObject;
                player.playerAudio.PlayWallRun();
                player.getNextWall = false;
                player.rigRotation = transform.rotation;
                player.wallRunVelocity = player.GetVelocity();
            }

            if(other.GetComponentInParent<MovingHazard>()!=null)
            {
                player.currentWallRunUpForce = -10;
            }
            if (isRightBox)
            {
                player.isWallRunning = true;
                player.isWallRunningRight = true;
                player.isWallRunningLeft = false;
            }

            else if (isLeftBox)
            {
                player.isWallRunning = true;
                player.isWallRunningLeft = true;
                player.isWallRunningRight = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.currentWall)
        {
            if (!wait)
            {
                player.canDoInput = false;
                canTakeNewWall = true;
                player.currentWall = null;
                wait = true;
                player.isWallRunning = false;
                player.isWallRunningLeft = false;
                player.isWallRunningRight = false;
                player.rigRotation = player.transform.rotation;
                player.SetLastWalls(other.gameObject);
                StartCoroutine(WaitCo(other));
            }
        }
    }

    private IEnumerator WaitCo(Collider other)
    {
        yield return new WaitForSeconds(0.2f);
        if (other.GetComponentInParent<WallRunCube>() != null)
        {
            other.GetComponentInParent<WallRunCube>().SetWallsTrue();
        }
        wait = false;
        player.getNextWall = true;
        player.canDoInput = true;
    }
}
