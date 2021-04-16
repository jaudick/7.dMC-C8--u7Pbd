using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    private void Update()
    {
        foreach(KeyCode code in System.Enum.GetValues((typeof(KeyCode))))
        {
            if(Input.GetKeyDown(code))
            {
                Debug.Log(code);
            }
        }
    }
}
