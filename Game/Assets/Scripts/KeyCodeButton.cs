using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyCodeButton : MonoBehaviour
{
    public bool keyEntered;
    public string key;

    private void Awake()
    {
        keyEntered = true;
    }
    public void EnterKey()
    {
        keyEntered = false;
    }

    private void Update()
    {
        if(!keyEntered)
        {
            foreach (KeyCode code in System.Enum.GetValues((typeof(KeyCode))))
            {
                if (Input.GetKey(code))
                {
                    GetComponentInChildren<Text>().text = code.ToString();
                    KeyBindingManager.instance.SetKeyCode(key, code);
                    keyEntered = true;
                }
            }
        }
    }

    public void Default()
    {
        KeyBindingManager.instance.SetDefault();
    }
}
