using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingManager : MonoBehaviour
{
    public static KeyBindingManager instance;
    public KeyCode JUMP;
    public KeyCode SLIDE;
    public KeyCode DASH_LEFT;
    public KeyCode DASH_RIGHT;
    public KeyCode TIME1;
    public KeyCode TIME2;
    public KeyCode TIME3;
    public KeyCode TIME4;
    public bool HOLD_WALL;
    public bool SCROLL_WHEEL;
    KeyCode[] codes;


    public Button[] keyButtons;
    public Toggle holdWallRun;
    public Toggle scrollWheel;

    public static List<string> defaultKeys = new List<string> { "Space", "LeftShift", "Q", "E", "Mouse0", "Mouse1", "Mouse3", "Mouse4" };
    public static List<bool> defaultPreferences = new List<bool> { false, true };
    public static List<string> currentKeys = new List<string> { "Space", "LeftShift", "Q", "E", "Mouse0", "Mouse1", "Mouse3", "Mouse4" };

    private void Awake()
    {
        instance = this;

        KeyBindData data = SaveKeyBindData.LoadInputBindings();

        currentKeys = data.GetInputs();
        HOLD_WALL = data.holdWallRun;
        SCROLL_WHEEL = data.scrollWheel;
        holdWallRun.isOn = HOLD_WALL;
        scrollWheel.isOn = SCROLL_WHEEL;

        JUMP = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[0]);
        SLIDE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[1]);
        DASH_LEFT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[2]);
        DASH_RIGHT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[3]);
        TIME1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[4]);
        TIME2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[5]);
        TIME3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[6]);
        TIME4 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[7]);
        codes = new KeyCode[] { JUMP, SLIDE, DASH_LEFT, DASH_RIGHT, TIME1, TIME2, TIME3, TIME4 };

        for (int i = 0; i<keyButtons.Length; i++)
        {
            keyButtons[i].GetComponentInChildren<Text>().text = currentKeys[i];
        }

    }


    public void Save()
    {
        KeyBindData data = new KeyBindData();
        data.SetInputs(currentKeys);
        data.holdWallRun = HOLD_WALL;
        data.scrollWheel = SCROLL_WHEEL;
        SaveKeyBindData.SaveDataToSystem(data);
    }

    public void SetDefault()
    {
        for (int i = 0; i < keyButtons.Length; i++)
        {
            currentKeys[i] = defaultKeys[i];
            keyButtons[i].GetComponentInChildren<Text>().text = currentKeys[i];
        }

        holdWallRun.isOn = false;
        scrollWheel.isOn = true;
        HOLD_WALL = false;
        SCROLL_WHEEL = true;

        JUMP = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[0]);
        SLIDE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[1]);
        DASH_LEFT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[2]);
        DASH_RIGHT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[3]);
        TIME1 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[4]);
        TIME2 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[5]);
        TIME3 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[6]);
        TIME4 = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[7]);
        Save();
    }

    public void SetWallToggle(bool toggle)
    {
        HOLD_WALL = toggle;
        Save();
    }
    public void SetScrollWheel(bool toggle)
    {
        SCROLL_WHEEL = toggle;
        Save();
    }


    public void SetKeyCode(string name, KeyCode code)
    {
        switch (name)
        {
            case "Jump":
                currentKeys[0] = code.ToString();
                JUMP = code;
                Save();
                break;
            case "Slide":
                currentKeys[1] = code.ToString();
                SLIDE = code;
                Save();
                break;
            case "DashLeft":
                currentKeys[2] = code.ToString();
                DASH_LEFT = code;
                Save();
                break;
            case "DashRight":
                currentKeys[3] = code.ToString();
                DASH_RIGHT = code;
                Save();
                break;
            case "Time1":
                currentKeys[4] = code.ToString();
                TIME1 = code;
                Save();
                break;
            case "Time2":
                currentKeys[5] = code.ToString();
                TIME2 = code;
                Save();
                break;
            case "Time3":
                currentKeys[6] = code.ToString();
                TIME3 = code;
                Save();
                break;
            case "Time4":
                currentKeys[7] = code.ToString();
                TIME4 = code;
                Save();
                break;
        }
        //savefile
    }

    


}
