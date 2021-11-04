using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using EasyRoads3Dv3;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CPTControl : MonoBehaviour
{
    // if trigger == true, trigger enter, otherwsie trigger is exited, thus no actions and feedbacks
    private bool trigger;

    // For making sound to be played just once as the target is collided
    private bool keyPressSound;

    // For storing the tag of the object triggered 
    private string objectTag;

    // For sending user enteries to the BehavouralData  
    private bool keyPressedOSign = false;
    private bool keyPressedXSign = false;
    //BehaviouralData dataValues;

    // Store the post object with which the CPT collider has collided
    private GameObject currentTrigger;

    // Calculate and save the response times
    private bool getUserResponse = false;
    private float timeElapsed;
    public SaveResponseTimeJSONvariables responseTimeJSON_X;
    public SaveResponseTimeJSONvariables responseTimeJSON_O;
    List<SaveResponseTimeJSONvariables> saveRTListData = new List<SaveResponseTimeJSONvariables>();
    public string jsonData;

    private float stopWatch;
    public string currentTime;
    public bool taskTimeControl = false;
    void Start()
    {
        // Get Component from the GameObject to which the BehaviouralData script is appended
        //dataValues = GameObject.Find("CPTPosts").GetComponent<BehaviouralData>();
        BehaviouralData.truePositives = 0;
        BehaviouralData.trueNegatives = 0;
        BehaviouralData.falseNegatives = 0;
        BehaviouralData.falsePositives = 0;
        BehaviouralData.TaskNo = 1;
    }
    private void Update()
    {
        if (taskTimeControl == true)
        {
            stopWatch = stopWatch + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(stopWatch);
            currentTime = time.ToString(@"mm\:ss\:fff");
        }
        Debug.Log(currentTime);
        // Only if the trigger == true and the objecTag == xsign or osign, make the functionalities work for the respective signs
        if (trigger == true && objectTag == "xsign")
        {
            responseTime();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                getUserResponse = false;
                Debug.Log("Reaction Time for X: " + timeElapsed);
                responseTimeJSON_X = new SaveResponseTimeJSONvariables();
                responseTimeJSON_X.postSign = "X-Sign";
                responseTimeJSON_X.response = "Incorrect";
                responseTimeJSON_X.responseTime = timeElapsed;
                responseTimeJSON_X.timeStamp = System.DateTime.UtcNow.ToString();
                responseTimeJSON_X.relative_time = currentTime;
                saveRTListData.Add(responseTimeJSON_X);

                // Change color on the posts as a respective feedback
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                if (keyPressSound == true)
                {
                    // Play the feedback sound just once
                    FindObjectOfType<AudioManager>().play("incorrectResponse");
                }
                keyPressSound = false;

                BehaviouralData.falsePositives = BehaviouralData.falsePositives + 1;
                Debug.Log("FALSE POSITIVES");
                keyPressedXSign = true;
            }
        }
        else if (trigger == true && objectTag == "osign")
        {
            responseTime();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                getUserResponse = false;
                responseTimeJSON_O = new SaveResponseTimeJSONvariables();
                responseTimeJSON_O.postSign = "O-Sign";
                responseTimeJSON_O.response = "Correct";
                responseTimeJSON_O.responseTime = timeElapsed;
                responseTimeJSON_O.timeStamp = System.DateTime.UtcNow.ToString();
                responseTimeJSON_O.relative_time = currentTime;
                Debug.Log("Reaction Time for O: " + timeElapsed);
                saveRTListData.Add(responseTimeJSON_O);

                // Change color on the posts as a respective feedback
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                if (keyPressSound == true)
                {
                    // Play the feedback sound just once 
                    FindObjectOfType<AudioManager>().play("correctResponse");
                }
                keyPressSound = false;

                BehaviouralData.truePositives = BehaviouralData.truePositives + 1;
                Debug.Log("TRUE POSITIVES");
                keyPressedOSign = true;
            }
        }
    }
    public void SaveJsonDataRT()
    {
        jsonData = JsonHelper.ToJson(saveRTListData.ToArray(), true);
        Debug.Log(jsonData);
        JSONSaveManager.Save(jsonData, SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        // As the object collides get the tag of the object with which collision happens
        Debug.Log("Hello");
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            trigger = true;
            keyPressSound = true;
            getUserResponse = true;
            Debug.Log("We have collided");

            // Make the respective sign visible on the post
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", new Color(153 / 255f, 28 / 255f, 28 / 255f));
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(153 / 255f, 28 / 255f, 28 / 255f));

            currentTrigger = other.gameObject;
            objectTag = other.gameObject.tag;

        }
    }

    private void responseTime() {
        Debug.Log("response time starts");
        if (getUserResponse == true)
        {
            timeElapsed = timeElapsed + Time.deltaTime * 1000;
        }
    }

    public void taskStopWatch()
    {
        taskTimeControl = true;
    }
   
    private void OnTriggerExit(Collider other)
    {
        // As the collision trigger is exited reset everything
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            if (keyPressedOSign == false && objectTag == "osign")
            {
                Debug.Log("FALSE NEGATIVE");
                BehaviouralData.falseNegatives = BehaviouralData.falseNegatives + 1;
            }
            if (keyPressedXSign == false && objectTag == "xsign")
            {
                Debug.Log("TRUE NEGATIVES");
                BehaviouralData.trueNegatives = BehaviouralData.trueNegatives + 1;
            }
            trigger = false;
            keyPressSound = true;

            // Make the sign on the collided post invisible 
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);

            keyPressedXSign = false;
            keyPressedOSign = false;
            Debug.Log(" no collision");
            timeElapsed = 0f;
        }
    }
}
