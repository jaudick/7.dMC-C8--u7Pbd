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
    public KeyCode PINK;
    public KeyCode BLUE;
    public KeyCode ORANGE;
    public KeyCode GREEN;
    public bool HOLD_WALL;
    public bool SCROLL_WHEEL;
    public bool COLOR_UI;
    public bool HEADBOB;
    public int SENSITIVITY;
    public int FOV;
    KeyCode[] codes;


    public Button[] keyButtons;
    public Toggle holdWallRun;
    public Toggle scrollWheel;
    public Toggle colorUIEnabled;
    public Toggle headbob;
    public Slider sensitivity;
    public Slider fov;
    public bool changeSensitivity;
    public bool changeFOV;

    public static List<string> defaultKeys = new List<string> { "Space", "LeftShift", "Q", "E", "Mouse0", "Mouse1", "Mouse4", "Mouse3" };
    public static List<bool> defaultPreferences = new List<bool> { false, true, true, true };
    private int defaultSensitivity = 5;
    private int defaultFOV = 118;
    public static List<string> currentKeys = new List<string> { "Space", "LeftShift", "Q", "E", "Mouse0", "Mouse1", "Mouse4", "Mouse3" };

    private void Awake()
    {

        instance = this;

        KeyBindData data = SaveKeyBindData.LoadInputBindings();

        currentKeys = data.GetInputs();
        HOLD_WALL = data.holdWallRun;
        SCROLL_WHEEL = data.scrollWheel;
        COLOR_UI = data.colorUI;
        HEADBOB = data.headbob;
        holdWallRun.isOn = HOLD_WALL;
        scrollWheel.isOn = SCROLL_WHEEL;
        colorUIEnabled.isOn = COLOR_UI;
        headbob.isOn = HEADBOB;
        SENSITIVITY = data.sensitivity;
        FOV = data.fov;

        JUMP = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[0]);
        SLIDE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[1]);
        DASH_LEFT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[2]);
        DASH_RIGHT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[3]);
        PINK = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[4]);
        BLUE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[5]);
        ORANGE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[6]);
        GREEN = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[7]);
        codes = new KeyCode[] { JUMP, SLIDE, DASH_LEFT, DASH_RIGHT, PINK, BLUE, ORANGE, GREEN };

        for (int i = 0; i < keyButtons.Length; i++)
        {
            keyButtons[i].GetComponentInChildren<Text>().text = currentKeys[i];
        }

        sensitivity.value = SENSITIVITY;
        fov.value = FOV;

    }


    public void Save()
    {
        KeyBindData data = new KeyBindData();
        data.SetInputs(currentKeys);
        data.holdWallRun = HOLD_WALL;
        data.scrollWheel = SCROLL_WHEEL;
        data.sensitivity = SENSITIVITY;
        data.headbob = HEADBOB;
        data.colorUI = COLOR_UI;
        data.fov = FOV;
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
        colorUIEnabled.isOn = true;
        headbob.isOn = true;
        HOLD_WALL = false;
        SCROLL_WHEEL = true;
        COLOR_UI = true;
        HEADBOB = true;
        SENSITIVITY = 5;
        FOV = 118;

        JUMP = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[0]);
        SLIDE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[1]);
        DASH_LEFT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[2]);
        DASH_RIGHT = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[3]);
        PINK = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[4]);
        BLUE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[5]);
        ORANGE = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[6]);
        GREEN = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentKeys[7]);
        SENSITIVITY = defaultSensitivity;
        FOV = defaultFOV;
        sensitivity.value = defaultSensitivity;
        fov.value = defaultFOV;
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

    public void SetColorUI(bool toggle)
    {
        COLOR_UI = toggle;
        Save();
    }

    public void SetHeadbob(bool toggle)
    {
        HEADBOB = toggle;
        Save();
    }

    public void SetSensitivity(int sensitivity)
    {
        SENSITIVITY = sensitivity;
        changeSensitivity = true;
        Save();
    }


    public void SetFOV(int sensitivity)
    {
        FOV = sensitivity;
        changeFOV = true;
        Save();
    }

    public void SetKeyCode(string name, KeyCode code)
    {
        switch (name)
        {
            case "Escape":
                break;
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
                PINK = code;
                Save();
                break;
            case "Time2":
                currentKeys[5] = code.ToString();
                BLUE = code;
                Save();
                break;
            case "Time3":
                currentKeys[6] = code.ToString();
                ORANGE = code;
                Save();
                break;
            case "Time4":
                currentKeys[7] = code.ToString();
                GREEN = code;
                Save();
                break;
        }
        //savefile
    }

    


}
