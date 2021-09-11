using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BehaviouralData : MonoBehaviour
{
    // Personal Data from the participant
    public static string userId;
    public static string userAge;
    public static string userGender;
    //public GameObject getUserId;
    public GameObject getUserAge;
    public GameObject getUserGender;

    public static int newId;

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

    public void getCurrentId(int id) {
        newId = id + 1;
        Debug.Log("We are getting the next id as "+newId);
    }
    public void firstID() {
        newId = 1;
        Debug.Log("We are getting the first id as " + newId);
    }
    public void UserDetails()
    {
        Debug.Log("We are inside user details");
        //Debug.Log(getUserName.GetComponent<TextMeshProUGUI>().text);
        userId = newId.ToString();
        userAge = getUserAge.GetComponent<TextMeshProUGUI>().text;
        userGender = getUserGender.GetComponent<TextMeshProUGUI>().text;
        Debug.Log("The user name is " + userId);
        Debug.Log("The user age is " + userAge);
        Debug.Log("The user gender is " + userGender);
    }

    public static void SaveData() { 
        CSVManager.AppendToFile(
            new string[7] { 
            userId,
            userAge,
            userGender,
            truePositives.ToString()+(" TP"),
            trueNegatives.ToString()+(" TN"),
            falsePositives.ToString()+(" FP"),
            falseNegatives.ToString()+(" FN")
            });
    }


}
