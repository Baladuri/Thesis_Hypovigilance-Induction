using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyRoads3Dv3;


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

    void Start()
    {
        // Get Component from the GameObject to which the BehaviouralData script is appended
        //dataValues = GameObject.Find("CPTPosts").GetComponent<BehaviouralData>();
    }
    private void Update()
    {
        // Only if the trigger == true and the objecTag == xsign or osign, make the functionalities work for the respective signs
        if (trigger == true && objectTag == "xsign")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
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

    private void OnTriggerEnter(Collider other)
    {
        // As the object collides get the tag of the object with which collision happens
        Debug.Log("Hello");
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            trigger = true;
            keyPressSound = true;
            Debug.Log("We have collided");

            // Make the respective sign visible on the post
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", new Color(153 / 255f, 28 / 255f, 28 / 255f));
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(153 / 255f, 28 / 255f, 28 / 255f));
            
            currentTrigger = other.gameObject;
            objectTag = other.gameObject.tag;

        }
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
        }
    }
}
