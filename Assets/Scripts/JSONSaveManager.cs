using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public static class JSONSaveManager
{
    public static string directory = "/JSONData/";
    public static string filename;

    public static void Save(string json, string sceneName)
    {
        if (sceneName == SceneManager.GetActiveScene().name) 
        {
            filename = "JsonResponseTime" + SceneManager.GetActiveScene().name + ".txt";
        }
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
           

       // string json = JsonHelper.ToJson(save);
        Debug.Log("writing to json");
       // string json = JsonUtility.ToJson(save);
        File.WriteAllText(directory + filename, json);
    }
    
}
