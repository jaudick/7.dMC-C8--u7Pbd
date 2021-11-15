using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyBindData
{
    public List<string> inputBindings;
    public bool holdWallRun;
    public bool scrollWheel;
    public bool colorUI;
    public bool headbob;
    public int sensitivity;
    public int fov;

    public KeyBindData()
    {
        inputBindings = new List<string>();
        holdWallRun = false;
        scrollWheel = true;
        colorUI = true;
        headbob = true;
        sensitivity = 5;
        fov = 118;
    }
    public List<string> GetInputs()
    {
        return inputBindings;
    }

    public void SetInputs(List<string> inputs)
    {
        inputBindings = inputs;
    }

    public void PrintInputs()
    {
        string fullInputList = "";
        foreach(string s in inputBindings)
        {
            fullInputList += s + " ";
        }
    }
}
