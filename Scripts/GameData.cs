using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData
{

    public static object load()
    {
        string filepath = Application.dataPath + "/highscores";

        if (File.Exists(filepath))
        {
            try
            {
                using (Stream stream = File.OpenRead(filepath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    object array =  formatter.Deserialize(stream);
                    return array;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        return null;
    }

    //public static void saveData<T>(T[] data)
    public static void saveData<T>(T[] data)
    {
        string path = Application.dataPath;
        /*StreamWriter sw = new StreamWriter(path);
        foreach (Transform t in myTransforms)
        {
            sw.WriteLine("" + t.name);
        }
        sw.Close();
        */
        using (Stream stream = File.OpenWrite(path + "/highscores"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }
}