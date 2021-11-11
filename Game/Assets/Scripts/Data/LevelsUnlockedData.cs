using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class LevelsUnlockedData
{
    public static void SaveDefaultDataToSystem()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ephemeralLevelUnlocked.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelUnlocker data = new LevelUnlocker();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveDataToSystem(int levels)
    {
        LevelUnlocker savedData = LoadLevelData();
        savedData.SetLevelsUnlocked(levels);

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/ephemeralLevelUnlocked.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, savedData);
        stream.Close();
    }

    public static LevelUnlocker LoadLevelData()
    {
        LevelUnlocker data;
        string path = Application.persistentDataPath + "/ephemeralLevelUnlocked.data";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0)
            {
                stream.Close();
                Debug.Log("<color=green>New Data File created</color>");
                SaveDefaultDataToSystem();
                data = new LevelUnlocker();
                
            }
            else
            {
                Debug.Log("<color=green>Save file exists</color>");
                data = formatter.Deserialize(stream) as LevelUnlocker;
                stream.Close();
            }

            return data;
        }
        else
        {
            Debug.LogError("<color=green>Save file not found at path: </color>" + path);
            SaveDefaultDataToSystem();
            return LoadLevelData();
        }
    }
}
