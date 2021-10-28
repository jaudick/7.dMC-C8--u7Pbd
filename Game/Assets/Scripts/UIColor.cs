using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColor : MonoBehaviour
{
    public GameObject ui;

    void Update()
    {
        ui.SetActive(KeyBindingManager.instance.COLOR_UI);
    }
}
