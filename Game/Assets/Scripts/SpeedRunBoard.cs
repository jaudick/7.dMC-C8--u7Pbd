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
            SpeedRunSaveData.SaveDefaultDataToSystem();
            LoadSpeedRunData();
            UpdateData();
        }
    }

    public void LoadSpeedRunData()
    {
        data = SpeedRunSaveData.LoadSpeedRunData();
    }

    public void UpdateData()
    {
        text.text = string.Format(
        "{0:00} : {1:00.000}\n" +
        "{2:00} : {3:00.000}\n" +
        "{4:00} : {5:00.000}\n" +
        "{6:00} : {7:00.000}\n" +
        "{8:00} : {9:00.000}\n" +
        "{10:00} : {11:00.000}\n" +
        "{12:00} : {13:00.000}\n" +
        "{14:00} : {15:00.000}\n" +
        "{16:00} : {17:00.000}\n" +
        "{18:00} : {19:00.000}\n" +
        "{20:00} : {21:00.000}\n",
        Mathf.FloorToInt(data.times[0] / 60), data.times[0] % 60,
        Mathf.FloorToInt(data.times[1] / 60), data.times[1] % 60,
        Mathf.FloorToInt(data.times[2] / 60), data.times[2] % 60,
        Mathf.FloorToInt(data.times[3] / 60), data.times[3] % 60,
        Mathf.FloorToInt(data.times[4] / 60), data.times[4] % 60,
        Mathf.FloorToInt(data.times[5] / 60), data.times[5] % 60,
        Mathf.FloorToInt(data.times[6] / 60), data.times[6] % 60,
        Mathf.FloorToInt(data.times[7] / 60), data.times[7] % 60,
        Mathf.FloorToInt(data.times[8] / 60), data.times[8] % 60,
        Mathf.FloorToInt(data.times[9] / 60), data.times[9] % 60,
        Mathf.FloorToInt(data.times[10] / 60), data.times[10] % 60);

    }
}
