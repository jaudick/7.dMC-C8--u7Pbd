using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] PlayerMovementRigidbody player;
    //public LayerMask layer;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            StopCoroutine(ChangeTempGroundCheck());
            player.tempIsGrounded = true;
            player.isGrounded = true;
        }

        /*
        else if (other.gameObject.CompareTag("WallRun"))
        {
            StopCoroutine(ChangeTempGroundCheck());
            player.tempIsGrounded = false;
            player.isGrounded = false;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            player.isGrounded = false;
            StartCoroutine(ChangeTempGroundCheck());
        }
    }

    private IEnumerator ChangeTempGroundCheck()
    {
        player.tempIsGrounded = true;
        yield return new WaitForSeconds(0.2f);
        player.tempIsGrounded = false;
    }
}
