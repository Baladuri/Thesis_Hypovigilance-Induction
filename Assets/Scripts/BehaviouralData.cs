using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BehaviouralData : MonoBehaviour
{
    // Personal Data from the participant
    public static string participantNo;
    public static string Age;
    public static string Gender;

    public static string userID;
    public static int TaskNo;

    public GameObject getParticipantNo;
    public GameObject getUserAge;
    public GameObject getUserGender;
    [SerializeField] private Slider _slider;
    [SerializeField] public TextMeshProUGUI _sliderRating;

    public static int newId;

    // Confusion Matrix values
    public static int truePositives = 0;
    public static int falsePositives = 0;
    public static int trueNegatives = 0;
    public static int falseNegatives = 0;

    public static string participantArousalRating;
    public static string TaskName;

    void Update()
    {
        /*Debug.Log("True Positives: " + truePositive);
        Debug.Log("False Positives: " + falsePositive);
        Debug.Log("True Negatives: " + trueNegative);
        Debug.Log("False Negatives: " + falseNegative);*/

        if (SceneManager.GetActiveScene().name == "StartMenu")
        {

        }
        else 
        {
            _slider.onValueChanged.AddListener((e) => {
                _sliderRating.text = e.ToString();
            });
        }   
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
        participantNo = getParticipantNo.GetComponent<TextMeshProUGUI>().text;
        Age = getUserAge.GetComponent<TextMeshProUGUI>().text;
        Gender = getUserGender.GetComponent<TextMeshProUGUI>().text;
        Debug.Log("The user name is " + participantNo);
        Debug.Log("The user age is " + Age);
        Debug.Log("The user gender is " + Gender);
    }

    public void SaveData() {
        userID = participantNo + Age + Gender + TaskNo.ToString();
        participantArousalRating = _sliderRating.text;
        TaskName = SceneManager.GetActiveScene().name;
        CSVManager.AppendToFile(
            new string[7] { 
            TaskName,
            userID,
            truePositives.ToString()+(" TP"),
            trueNegatives.ToString()+(" TN"),
            falsePositives.ToString()+(" FP"),
            falseNegatives.ToString()+(" FN"),
            ("Arousal level: ")+participantArousalRating+("  ")
            });
    }


}
