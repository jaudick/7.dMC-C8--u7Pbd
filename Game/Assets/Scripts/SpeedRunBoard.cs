using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpeedRunBoard : MonoBehaviour
{
    SpeedRunData data;
    public Text text;

    private void Awake()
    {
        string path = Application.persistentDataPath + "/ephemeralSpeedrun.data";
        if (File.Exists(path))
        {
            Debug.Log(path);
            LoadSpeedRunData();
            UpdateData();
        }
        else
        {
            Debug.Log("Creating new default data");
            SaveData.SaveDefaultDataToSystem();
            LoadSpeedRunData();
            UpdateData();
        }
    }

    public void LoadSpeedRunData()
    {
        data = SaveData.LoadSpeedRunData();
    }

    public void UpdateData()
    {
        Debug.Log("Updating SpeedRun Board " + data.times[4]);
        text.text = string.Format(
        "{0:0.000}\n" +
        "{1:0.000}\n" +
        "{2:0.000}\n" +
        "{3:0.000}\n" +
        "{4:0.000}\n" +
        "{5:0.000}\n" +
        "{6:0.000}\n" +
        "{7:0.000}\n" +
        "{8:0.000}\n" +
        "{9:0.000}\n" +
        "{10:0.000}", 
        data.times[0],data.times[1],data.times[2],data.times[3],data.times[4],data.times[5],data.times[6],data.times[7],data.times[8],data.times[9],data.times[10]);
    }
}
