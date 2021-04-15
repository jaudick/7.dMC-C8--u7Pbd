using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingManager : MonoBehaviour
{
    public KeyBindingManager KBM;
    public KeyCode jump_KBM;
    public KeyCode slide_KBM;
    public KeyCode dashLeft_KBM;
    public KeyCode dashRight_KBM;
    public KeyCode Time1_KBM;
    public KeyCode Time2_KBM;
    public KeyCode Time3_KBM;
    public KeyCode Time4_KBM;

    public List<Dropdown> dropdowns = new List<Dropdown>();
    public static List<string> keys = new List<string> { "LeftMouse", "RightMouse", "ThumbBack" , "ThumbFront", "Space", "LeftShift", "LeftCtrl", "LeftAlt", "CapsLock", "Tab", "BackQuote", "Q", "E", "R", "T", "F", "Z", "X", "C", "V"};

    private void Awake()
    {
        if(KBM == null)
        {
            DontDestroyOnLoad(gameObject);
            KBM = this;
        }
        else if(KBM != this)
        {
            Destroy(gameObject);
        }    

        for(int i = 0; i<dropdowns.Count; i++)
        {
            dropdowns[i].AddOptions(keys);
        }
    }

    public void ChangeJumpKey(int id)
    {
        jump_KBM = GetKeyCode(id);
    }

    public void ChangeSlideKey(int id)
    {
        slide_KBM = GetKeyCode(id);
    }

    public void ChangeDashLeftKey(int id)
    {
        dashLeft_KBM = GetKeyCode(id);
    }

    public void ChangeDashRightKey(int id)
    {
        dashRight_KBM = GetKeyCode(id);
    }

    public void ChangeTime1Key(int id)
    {
        Time1_KBM= GetKeyCode(id);
    }

    public void ChangeTime2Key(int id)
    {
        Time2_KBM = GetKeyCode(id);
    }

    public void ChangeTime3Key(int id)
    {
        Time3_KBM = GetKeyCode(id);
    }
    public void ChangeTime4Key(int id)
    {
        Time4_KBM = GetKeyCode(id);
    }


    private KeyCode GetKeyCode(int id)
    {
        if (keys[id] == "LeftMouse" || keys[id] == "RightMouse" || keys[id] == "ThumbBack" || keys[id] == "ThumbFront")
        {
            Debug.Log("MouseInput");
            return 0;
        }
        else
        {
            return (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[id]);
        }
    }
}
