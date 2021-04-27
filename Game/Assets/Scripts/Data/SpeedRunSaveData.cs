using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SpeedRunSaveData
{
    public static void SaveDefaultDataToSystem()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ephemeralSpeedrun.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SpeedRunData data = new SpeedRunData();
        Debug.Log("<color=green>New path created</color>");

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveDataToSystem(int level, float time)
    {
        SpeedRunData savedData = LoadSpeedRunData();
        savedData.AssignLevelTime(level, time);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ephemeralSpeedrun.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, savedData);
        stream.Close();
    }

    public static SpeedRunData LoadSpeedRunData()
    {
        SpeedRunData data;
        string path = Application.persistentDataPath + "/ephemeralSpeedrun.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0)
            {
                stream.Close();
                Debug.Log("<color=green>New Data File created</color>");
                SaveDefaultDataToSystem();
                data = new SpeedRunData();
                
            }
            else
            {
                //Debug.Log("<color=green>Save file exists</color>");
                data = formatter.Deserialize(stream) as SpeedRunData;
                stream.Close();
            }

            return data;
        }
        else
        {
            Debug.LogError("<color=green>Save file not found at path: </color>" + path);
            SaveDefaultDataToSystem();
            return null;
        }
    }
}
