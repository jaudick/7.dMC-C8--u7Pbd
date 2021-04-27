using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunBox : MonoBehaviour
{
    private PlayerMovementRigidbody player;
    public bool isRightBox;
    public bool isLeftBox;
    int layer = 1 << 10;
    private bool wait = false;

    private void Start()
    {
        player = GetComponentInParent<PlayerMovementRigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (player.isTouchingWall && !wait && player.canDoInput && (other.CompareTag("WallRun") && !player.isGrounded && ((player.lastWall == null) || player.lastWall!=other.gameObject)) 
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
        if (other.gameObject == player.currentWall && (player.isWallRunning || player.justJumpedOffWall))
        {
            if (!wait)
            {
                player.canDoInput = false;
                player.currentWall = null;
                wait = true;
                player.isWallRunning = false;
                player.isWallRunningLeft = false;
                player.isWallRunningRight = false;
                player.rigRotation = player.transform.rotation;
                if (other.GetComponent<Mobius>() == null)
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
