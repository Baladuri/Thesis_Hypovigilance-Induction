using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public static class SavePupilData
{
    public static string directory = "PupilJsonData";
    public static string filename = "trying.txt";

    static string GetDirectoryPath() 
    {
        return Application.persistentDataPath + "/Resources/" + directory;
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
        using (StreamWriter sw = File.CreateText(GetDirectoryPath()))
        {
            string finalString = "Json data for pupil diameters";
            sw.WriteLine(finalString);
        }
    }

    public static void AppendToFile(string json)
    {
        /*if (sceneName == "New Simple Task")
        {
            Debug.Log("Hurrey We did it");
            //filename = "PupilDataJSON" + SceneManager.GetActiveScene().name + ".txt";
        }*/
        VerifyDirectory();
        VerifyFile();
        /*using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            sw.WriteLine(json);
        }*/
    }
    
}
