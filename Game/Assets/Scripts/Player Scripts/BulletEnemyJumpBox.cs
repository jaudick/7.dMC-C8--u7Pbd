using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyJumpBox : MonoBehaviour
{
    public bool canJumpOffEnemy = false;
    int layerMask = 1 << 8;

    private void Update()
    {
        Collider[] enemies = Physics.OverlapBox(transform.position, new Vector3(0.625f*2, 0.218f*3, 0.625f*2), Quaternion.identity, layerMask);
        if (enemies.Length > 0)
            canJumpOffEnemy = true;
        else
            canJumpOffEnemy = false;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<JumpOffEnemyBox>() != null && other.gameObject.layer == 8 && (transform.parent.position.y > other.transform.position.y))
        {
            canJumpOffEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<JumpOffEnemyBox>() != null && other.gameObject.layer == 8)
        {
            canJumpOffEnemy = false;
        }
    }*/
}
