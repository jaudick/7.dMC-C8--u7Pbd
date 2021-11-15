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

    public void ColorUIToggle(bool toggle)
    {
        KeyBindingManager.instance.SetColorUI(toggle);
    }

    public void HeadbobToggle(bool toggle)
    {
        KeyBindingManager.instance.SetHeadbob(toggle);
    }
}
