using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveKeyBindData
{
    public static KeyBindData LoadInputBindings()
    {
        KeyBindData dataFile = new KeyBindData();
        string path = Application.persistentDataPath + "/KeyBindData.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            if (stream.Length == 0)
            {
                stream.Close();
                Debug.Log("<color=green>New Data File created</color>");
                SaveDefaultDataToSystem(dataFile);
                dataFile.SetInputs(KeyBindingManager.defaultKeys);

            }
            else
            {
                Debug.Log("<color=green>Save file exists</color> at path: " + path);
                dataFile = formatter.Deserialize(stream) as KeyBindData;
                stream.Close();
            }

            return dataFile;
        }
        else
        {
            Debug.LogError("<color=green>Save file not found at path: </color>" + path);
            SaveDefaultDataToSystem(dataFile);
            dataFile.SetInputs(KeyBindingManager.defaultKeys);
            return dataFile;
        }
    }

    public static void SaveDataToSystem(KeyBindData speedRunData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/KeyBindData.data";
        Debug.Log("<color=yellow>Saving new key binds</color> ");
        speedRunData.PrintInputs();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, speedRunData);
        stream.Close();
    }

    public static void SaveDefaultDataToSystem(KeyBindData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/KeyBindData.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        data.SetInputs(KeyBindingManager.defaultKeys);
        Debug.Log("<color=green>New path created</color>");

        formatter.Serialize(stream, data);
        stream.Close();
    }
}
