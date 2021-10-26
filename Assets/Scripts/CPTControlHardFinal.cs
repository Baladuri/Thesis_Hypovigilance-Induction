using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPTControlHardFinal : MonoBehaviour
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
    private bool keyPressedXSignExcep = false;
    private bool keyPressedOSignExcep = false;
    private bool normalCases = false;
    private bool excepCases = false;
    BehaviouralData dataValues;

    // Store the post object with which the CPT collider has collided
    private GameObject currentTrigger;

    // For implementing hard task, after every 3 circles skip 4th circle, and for crosses while skipping 2 crosses press space for 3rd cross
    // Boolean to check how many times both have been triggered
    private bool circlesTriggerLimit;
    private bool crossesTriggerLimit;

    // Store instances of the triggers
    private int circlesTriggerOccurances= 1;
    private int crossesTriggerOccurances = 1;

    // To prevent repeative incrementation in update method
    private bool triggerOnceCross = true;
    private bool triggerOnceCircle = true;

    // Calculte and save the response time
    private bool getUserResponse = false;
    private float timeElapsed;
    public SaveResponseTimeJSONvariables responseTimeJSON_X;
    public SaveResponseTimeJSONvariables responseTimeJSON_O;
    public SaveResponseTimeJSONvariables responseTimeJSON_X_correct;
    public SaveResponseTimeJSONvariables responseTimeJSON_O_incorrect;
    List<SaveResponseTimeJSONvariables> saveRTListData = new List<SaveResponseTimeJSONvariables>();
    public string jsonData;

    void Start()
    {
        // Get Component from the GameObject to which the BehaviouralData script is appended
        //dataValues = GameObject.Find("CPTPosts").GetComponent<BehaviouralData>();
        BehaviouralData.truePositives = 0;
        BehaviouralData.trueNegatives = 0;
        BehaviouralData.falseNegatives = 0;
        BehaviouralData.falsePositives = 0;
        BehaviouralData.TaskNo = 2;
    }
    private void Update()
    {
        // Only if the trigger == true and the objecTag == xsign or osign, make the functionalities work for the respective signs
        if (trigger == true) 
        {
            if (crossesTriggerLimit == false && objectTag == "xsign")
            {
                normalCases = true;
                responseTime();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    getUserResponse = false;
                    Debug.Log("Reaction Time for X Red: " + timeElapsed);
                    responseTimeJSON_X = new SaveResponseTimeJSONvariables();
                    responseTimeJSON_X.postSign = "X-Sign";
                    responseTimeJSON_X.response = "Incorrect";
                    responseTimeJSON_X.responseTime = timeElapsed;
                    responseTimeJSON_X.timeStamp = System.DateTime.UtcNow.ToString();
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

                    //dataValues.falsePositive = dataValues.falsePositive + 1;
                    keyPressedXSign = true;
                }               
                if (triggerOnceCross == true)
                {
                    Debug.Log("crosses Triggered: " + crossesTriggerOccurances);
                    crossesTriggerOccurances++;
                }
                triggerOnceCross = false;
            }
            else if (crossesTriggerLimit == true && objectTag == "xsign")
            {
                excepCases = true;
                responseTime();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    getUserResponse = false;
                    Debug.Log("Reaction Time for X Green: " + timeElapsed);
                    responseTimeJSON_X_correct = new SaveResponseTimeJSONvariables();
                    responseTimeJSON_X_correct.postSign = "X-Sign";
                    responseTimeJSON_X_correct.response = "Correct";
                    responseTimeJSON_X_correct.responseTime = timeElapsed;
                    responseTimeJSON_X_correct.timeStamp = System.DateTime.UtcNow.ToString();
                    saveRTListData.Add(responseTimeJSON_X_correct);

                    // Change color on the posts as a respective feedback
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                    if (keyPressSound == true)
                    {
                        // Play the feedback sound just once
                        FindObjectOfType<AudioManager>().play("correctResponse");
                    }
                    keyPressSound = false;

                    //dataValues.falsePositive = dataValues.falsePositive + 1;
                    keyPressedXSignExcep = true;                                     
                }
                Debug.Log("crosses Triggered: " + crossesTriggerOccurances);
                crossesTriggerOccurances = 1;
            }
            else if (circlesTriggerLimit == false && objectTag == "osign")
            {
                normalCases = true;
                responseTime();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    getUserResponse = false;
                    Debug.Log("Reaction Time for O Green: " + timeElapsed);
                    responseTimeJSON_O = new SaveResponseTimeJSONvariables();
                    responseTimeJSON_O.postSign = "O-Sign";
                    responseTimeJSON_O.response = "Correct";
                    responseTimeJSON_O.responseTime = timeElapsed;
                    responseTimeJSON_O.timeStamp = System.DateTime.UtcNow.ToString();
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

                    //dataValues.truePositive = dataValues.truePositive + 1;
                    keyPressedOSign = true;
              
                }
                if (triggerOnceCircle == true) 
                {
                    Debug.Log("circles Triggered: " + circlesTriggerOccurances);
                    circlesTriggerOccurances++;
                }
                triggerOnceCircle = false;
            }
            else if (circlesTriggerLimit == true && objectTag == "osign")
            {
                excepCases = true;
                responseTime();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    getUserResponse = false;
                    Debug.Log("Reaction Time for O Red: " + timeElapsed);
                    responseTimeJSON_O_incorrect = new SaveResponseTimeJSONvariables();
                    responseTimeJSON_O_incorrect.postSign = "O-Sign";
                    responseTimeJSON_O_incorrect.response = "Incorrect";
                    responseTimeJSON_O_incorrect.responseTime = timeElapsed;
                    responseTimeJSON_O_incorrect.timeStamp = System.DateTime.UtcNow.ToString();
                    saveRTListData.Add(responseTimeJSON_O_incorrect);


                    // Change color on the posts as a respective feedback
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                    if (keyPressSound == true)
                    {
                        // Play the feedback sound just once 
                        FindObjectOfType<AudioManager>().play("incorrectResponse");
                    }
                    keyPressSound = false;

                    //dataValues.truePositive = dataValues.truePositive + 1;
                    keyPressedOSignExcep = true;
                   
                }
                Debug.Log("circles Triggered: " + circlesTriggerOccurances);
                circlesTriggerOccurances = 1;
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
        //Debug.Log("Hello");
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            trigger = true;
            keyPressSound = true;
            getUserResponse = true;
            //Debug.Log("We have collided");

            // Make the respective sign visible on the post
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", new Color(153 / 255f, 28 / 255f, 28 / 255f));
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(153 / 255f, 28 / 255f, 28 / 255f));

            currentTrigger = other.gameObject;
            objectTag = other.gameObject.tag;

            if (crossesTriggerOccurances > 2)
            {
                crossesTriggerLimit = true;
            }
            else {
                crossesTriggerLimit = false;
            }
            if (circlesTriggerOccurances > 3)
            {
                circlesTriggerLimit = true;
            }
            else {
                circlesTriggerLimit = false;
            }

        }
    }

    private void responseTime()
    {
        Debug.Log("response time starts");
        if (getUserResponse == true)
        {
            timeElapsed = timeElapsed + Time.deltaTime * 1000;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // As the collision trigger is exited reset everything
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            if (normalCases == true)
            {
                if (keyPressedXSign == false && objectTag == "xsign")
                {
                    Debug.Log("True Negatives");
                    BehaviouralData.trueNegatives = BehaviouralData.trueNegatives + 1;
                }
                else if (keyPressedXSign == true && objectTag == "xsign")
                {
                    Debug.Log("False Positives");
                    BehaviouralData.falsePositives = BehaviouralData.falsePositives + 1;
                }
                else if (keyPressedOSign == false && objectTag == "osign")
                {
                    Debug.Log("False Negatives");
                    BehaviouralData.falseNegatives = BehaviouralData.falseNegatives + 1;
                }
                else if (keyPressedOSign == true && objectTag == "osign")
                {
                    Debug.Log("True Positive");
                    BehaviouralData.truePositives = BehaviouralData.truePositives + 1;
                }
            }
            else if (excepCases == true)
            {
                if (keyPressedOSignExcep == false && objectTag == "osign")
                {
                    Debug.Log("True Negatives");
                    BehaviouralData.trueNegatives = BehaviouralData.trueNegatives + 1;
                }
                else if (keyPressedOSignExcep == true && objectTag == "osign")
                {
                    Debug.Log("False Positives");
                    BehaviouralData.falsePositives = BehaviouralData.falsePositives + 1;
                }
                else if (keyPressedXSignExcep == false && objectTag == "xsign")
                {
                    Debug.Log("False Negatives");
                    BehaviouralData.falseNegatives = BehaviouralData.falseNegatives + 1;
                }
                else if (keyPressedXSignExcep == true && objectTag == "xsign")
                {
                    Debug.Log("True Positive");
                    BehaviouralData.truePositives = BehaviouralData.truePositives + 1;
                }
            }
            trigger = false;
            keyPressSound = true;

            // Make the sign on the collided post invisible 
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);

            keyPressedXSign = false;
            keyPressedOSign = false;
            keyPressedXSignExcep = false;
            keyPressedOSignExcep = false;
            normalCases = false;
            excepCases = false;
            //Debug.Log(" no collision");

            triggerOnceCircle = true;
            triggerOnceCross = true;

            timeElapsed = 0f;
        }
    }
}
