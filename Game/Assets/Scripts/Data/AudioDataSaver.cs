using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class AudioDataSaver
{
    public static AudioData LoadAudioData()
    {
        AudioData dataFile = new AudioData();
        string path = Application.persistentDataPath + "/AudioVolume.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0)
            {
                stream.Close();
                Debug.Log("<color=green>New Data File created</color>");
                SaveDefaultDataToSystem(dataFile);

            }
            else
            {
                //Debug.Log("<color=green>Save file exists</color> at path: " + path);
                dataFile = formatter.Deserialize(stream) as AudioData;
                stream.Close();
            }

            return dataFile;
        }
        else
        {
            Debug.LogError("<color=green>Save file not found at path: </color>" + path);
            SaveDefaultDataToSystem(dataFile);
            return dataFile;
        }
    }

    public static void SaveDataToSystem(AudioData audioData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/AudioVolume.data";
        //Debug.Log("<color=yellow>Saving new key binds</color> ");
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, audioData);
        stream.Close();
    }

    public static void SaveDefaultDataToSystem(AudioData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/AudioVolume.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        Debug.Log("<color=green>New path created</color>");

        formatter.Serialize(stream, data);
        stream.Close();
    }
}
