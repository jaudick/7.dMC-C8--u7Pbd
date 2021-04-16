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
    public bool holdWallEnabled;
    public bool scrollWheelEnabled;
    public bool doInitialUpdate;
    private KeyCode[] codes;

    public static List<string> defaultKeys = new List<string> { "Space", "LeftShift", "Q", "E", "LeftMouse", "RightMouse", "ThumbBack", "ThumbFront" };
    public static List<string> defaultPreferences = new List<string> { "Disabled", "Enabled" };
    public static List<string> currentKeys = new List<string> { "Space", "LeftShift", "Q", "E", "LeftMouse", "RightMouse", "ThumbBack", "ThumbFront" };

    public List<Dropdown> bindings = new List<Dropdown>();
    public Dropdown holdWall;
    public Dropdown scrollWheel;
    public static List<string> keys = new List<string> { "LeftMouse", "MiddleMouse", "RightMouse", "ThumbBack" , "ThumbFront", "Space", "LeftAlt", "LeftCtrl", "LeftShift", "CapsLock", "Tab", "BackQuote", "Q", "E", "R", "T", "F", "G", "V", "C", "X", "Z"};
    public static List<string> preferenceBools = new List<string> { "Enabled", "Disabled" };

    private void Awake()
    {
        codes = new KeyCode[] { jump_KBM, slide_KBM, dashLeft_KBM,dashRight_KBM,Time1_KBM,Time2_KBM,Time3_KBM,Time4_KBM };
        doInitialUpdate = true;

        if(KBM == null)
        {
            DontDestroyOnLoad(gameObject);
            KBM = this;
        }
        else if(KBM != this)
        {
            Destroy(gameObject);
        }    

        for(int i = 0; i<bindings.Count; i++)
        {
            bindings[i].AddOptions(keys);
        }

        holdWall.AddOptions(preferenceBools);
        scrollWheel.AddOptions(preferenceBools);

        //load the file.
        //set the currentkeys with the file.

        jump_KBM = ProcessKeyCode(currentKeys[0]);
        slide_KBM = ProcessKeyCode(currentKeys[1]);
        dashLeft_KBM = ProcessKeyCode(currentKeys[2]);
        dashRight_KBM = ProcessKeyCode(currentKeys[3]);
        Time1_KBM = ProcessKeyCode(currentKeys[4]);
        Time2_KBM = ProcessKeyCode(currentKeys[5]);
        Time3_KBM = ProcessKeyCode(currentKeys[6]);
        Time4_KBM = ProcessKeyCode(currentKeys[7]);

    }

    private KeyCode ProcessKeyCode(string input)
    {
        switch (input)
        {
            case "LeftMouse":
                return KeyCode.Mouse0;
            case "RightMouse":
                return KeyCode.Mouse1;
            case "MiddleMouse":
                return KeyCode.Mouse2;
            case "ThumbBack":
                return KeyCode.Mouse3;
            case "ThumbFront":
                return KeyCode.Mouse4;
            case "LeftCtrl":
                return KeyCode.LeftControl;
            default:
                return (KeyCode)System.Enum.Parse(typeof(KeyCode), input);
        }
    }


    private void Update()
    {
        if (doInitialUpdate)
        {
            for (int i = 0; i < bindings.Count; i++)
            {
                bindings[i].GetComponentInChildren<Text>().text = currentKeys[i];
            }
            doInitialUpdate = false;
        }
    }

    public void Save()
    {

    }

    public void SetDefault()
    {
        for(int i = 0; i < defaultKeys.Count; i++)
        {
            currentKeys[i] = defaultKeys[i];
            bindings[i].GetComponentInChildren<Text>().text = currentKeys[i];
        }
        holdWallEnabled = false;
        scrollWheelEnabled = true;
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

    public void ChangeHoldWallRunPreference(int id)
    {
        holdWallEnabled = preferenceBools[id] == "Enabled" ? true : false ;
    }

    public void ChangeScrollWheelPreference(int id)
    {
        scrollWheelEnabled = preferenceBools[id] == "Enabled" ? true : false;
    }


    private KeyCode GetKeyCode(int id)
    {
        if (keys[id] == "LeftMouse" || keys[id] == "RightMouse" || keys[id] == "ThumbBack" || keys[id] == "ThumbFront" || keys[id] == "MiddleMouse" || keys[id] == "LeftCtrl")
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
