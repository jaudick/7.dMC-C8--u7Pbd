using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorsLock : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
