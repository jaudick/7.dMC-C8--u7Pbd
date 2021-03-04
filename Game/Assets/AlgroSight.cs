using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgroSight : MonoBehaviour
{
    public Algro a;
    private void OnTriggerEnter(Collider other)
    {
        if (!a.occupied && other.tag == "Player")
        {
            a.targ = other.gameObject;
            a.occupied = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (a.occupied && other.tag == "Player") a.occupied = false;
        a.reload = 2;
    }
}