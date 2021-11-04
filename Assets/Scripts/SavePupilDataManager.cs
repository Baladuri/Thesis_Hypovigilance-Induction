using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public static class SavePupilDataManager
{
    public static string directory = "Pupil Json Data";
    public static string filename = "trying.txt";

    static string GetDirectoryPath() 
    {
        return Application.dataPath + "/Resources/" + directory;
    }

    static string GetFilePath() 
    {
        return GetDirectoryPath() + "/" + filename;
    }

    static void VerifyDirectory() 
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file)) 
        {
            CreateFile();
        }
    }

    public static void CreateFile()
    {
        VerifyDirectory();
        Debug.Log("Lets create file");
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "Json data for pupil diameters";
            sw.WriteLine(finalString);
        }
    }

    public static void AppendToFile(string json, string sceneName)
    {
        if (sceneName == SceneManager.GetActiveScene().name)
        {
            filename = "PupilDataJSON" + SceneManager.GetActiveScene().name + ".txt";
        }
        VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            sw.WriteLine(json);
        }
    }
    
}
