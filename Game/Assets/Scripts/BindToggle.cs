using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindToggle : MonoBehaviour
{
    public void HoldWallToggle(bool toggle)
    {
        KeyBindingManager.instance.SetWallToggle(toggle);
    }

    public void ScrollWheelToggle(bool toggle)
    {
        KeyBindingManager.instance.SetScrollWheel(toggle);
    }
}
