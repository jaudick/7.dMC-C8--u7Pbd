using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensitivity : MonoBehaviour
{
    public void ChangeSensitivity(float number)
    {
        KeyBindingManager.instance.SetSensitivity((int) Mathf.RoundToInt(number));
    }
}
