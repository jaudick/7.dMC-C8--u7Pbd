using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    public GameObject thisMenuOption;
    public GameObject menuOptionToGoTo;

    public void Change()
    {
        thisMenuOption.SetActive(false);
        menuOptionToGoTo.SetActive(true);
    }
}
