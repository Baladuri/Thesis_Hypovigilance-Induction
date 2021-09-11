using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class CSVManager
{
    private static string dataFolderName = "User Data";
    private static string dataFileName = "userData.csv";
    private static string dataSeparator = ",";
    private static string[] fileHeaders = new string[7] {
        "id",
        "Age",
        "Gender",
        "True Positives",
        "True Negatives",
        "False Positives",
        "False Negatives"
    };
    private static string timeStampHeader = "time stamp";

    public static void AppendToFile(string[] strings)
    {
        //VerifyDirectory();
        //VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++) 
            {
                if (finalString != "")
                {
                    finalString += dataSeparator;
                }
                finalString += strings[i];
            }
            finalString += GetTimeStamp();
            sw.WriteLine(finalString);
        }
    }
    public static void CreateFile()
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < fileHeaders.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += dataSeparator;
                }
                finalString += fileHeaders[i];
            }
            finalString += dataSeparator + timeStampHeader;
            sw.WriteLine(finalString);
        }
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

    static string GetDirectoryPath() 
    {
        return Application.dataPath + "/Resources/" + dataFolderName;
    }

    static string GetFilePath()
    {
        return GetDirectoryPath() + "/" + dataFileName;
    }

    static string GetTimeStamp()
    {
        return System.DateTime.UtcNow.ToString();
    }
}
