using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BehaviouralData : MonoBehaviour
{
    // Personal Data from the participant
    public static string userName;
    public static string userAge;
    public static string userGender;
    public GameObject getUserName;
    public GameObject getUserAge;
    public GameObject getUserGender;

    // Confusion Matrix values
    public static int truePositives = 0;
    public static int falsePositives = 0;
    public static int trueNegatives = 0;
    public static int falseNegatives = 0;
  
    void Update()
    {
        /*Debug.Log("True Positives: " + truePositive);
        Debug.Log("False Positives: " + falsePositive);
        Debug.Log("True Negatives: " + trueNegative);
        Debug.Log("False Negatives: " + falseNegative);*/
    }
 
    public void UserDetails()
    {
        Debug.Log("We are inside user details");
        //Debug.Log(getUserName.GetComponent<TextMeshProUGUI>().text);
        userName = getUserName.GetComponent<TextMeshProUGUI>().text;
        userAge = getUserAge.GetComponent<TextMeshProUGUI>().text;
        userGender = getUserGender.GetComponent<TextMeshProUGUI>().text;
        Debug.Log("The user name is " + userName);
        Debug.Log("The user age is " + userAge);
        Debug.Log("The user gender is " + userGender);
    }

    public static void SaveData() { 
        CSVManager.AppendToFile(
            new string[7] { 
            userName,
            userAge,
            userGender,
            truePositives.ToString()+(" TP"),
            trueNegatives.ToString()+(" TN"),
            falsePositives.ToString()+(" FP"),
            falseNegatives.ToString()+(" FN")
            });
    }


}
