using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunBox : MonoBehaviour
{
    private PlayerMovementRigidbody player;
    public bool isRightBox;
    public bool isLeftBox;
    int layer = 1 << 10;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementRigidbody>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WallRun") && !player.isGrounded && ((player.lastWall == null) || player.lastWall!=other.gameObject) && 
            (((KeyBindingManager.instance.HOLD_WALL && (Input.GetKey(KeyBindingManager.instance.JUMP) || Input.GetAxis("JumpController") > 0))) || !KeyBindingManager.instance.HOLD_WALL))
        {
            if (Input.GetAxis("JumpController") > 0)
                player.lastFrameWasHoldingRightTigger = true;
            if (player.getNextWall)
            {
                player.SetLastWalls(other.gameObject);
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
        if (other.CompareTag("WallRun") && !player.isGrounded)
        {
            player.isWallRunning = false;
            player.isWallRunningLeft = false;
            player.isWallRunningRight = false;
            player.rigRotation = player.transform.rotation;
            player.getNextWall = true;
        }
    }
}
