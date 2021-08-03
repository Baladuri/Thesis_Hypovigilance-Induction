using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPTControlHard : MonoBehaviour
{
    // if trigger == 1, trigger enter, otherwsie trigger is exited, thus no actions and feedbacks
    private bool trigger;

    // For making sound to be played just once as the target is collided
    private bool keyPressSound;

    // For storing the tag of the object triggered 
    private string objectTag;

    // For sending user enteries to the BehavouralData  
    private bool keyPressedOSign = false;
    private bool keyPressedXSign = false;
    BehaviouralData dataValues;

    // Store the post object with which the CPT collider has collided
    private GameObject currentTrigger;


    //trying...
    private bool nextTrigger;
    private string parentTag;
    private string previousParentTag;

    void Start()
    {
        // Get Component from the GameObject to which the BehaviouralData script is appended
        //dataValues = GameObject.Find("CPTPosts").GetComponent<BehaviouralData>();
    }
    private void Update()
    {
        // trying.............
        if (trigger == true) 
        {
            if (nextTrigger == true && objectTag == "osign")
            {
                if (parentTag != previousParentTag)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        // Change color on the posts as a respective feedback
                        currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                        currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                        if (keyPressSound == true)
                        {
                            // Play the feedback sound just once
                            FindObjectOfType<AudioManager>().play("pressedCross");
                        }
                        keyPressSound = false;
                    }
                }
                else if (parentTag == previousParentTag)
                {

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        // Change color on the posts as a respective feedback
                        currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                        currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                        if (keyPressSound == true)
                        {
                            // Play the feedback sound just once 
                            FindObjectOfType<AudioManager>().play("pressedCircle");
                        }
                        keyPressSound = false;

                        // dataValues.truePositive = dataValues.truePositive + 1;
                        //keyPressedOSign = true;
                    }
                }
            }
            



            // Only if the trigger == true and the objecTag == xsign or osign, make the functionalities work for the respective signs
            if (objectTag == "xsign")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Change color on the posts as a respective feedback
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);

                    if (keyPressSound == true)
                    {
                        // Play the feedback sound just once
                        FindObjectOfType<AudioManager>().play("pressedCross");
                    }
                    keyPressSound = false;

                    // dataValues.falsePositive = dataValues.falsePositive + 1;
                    keyPressedXSign = true;
                }
            }
            else if (objectTag == "osign" && nextTrigger == false)
            {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Change color on the posts as a respective feedback
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    currentTrigger.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);

                    if (keyPressSound == true)
                    {
                        // Play the feedback sound just once 
                        FindObjectOfType<AudioManager>().play("pressedCircle");
                    }
                    keyPressSound = false;

                    // dataValues.truePositive = dataValues.truePositive + 1;
                    keyPressedOSign = true;
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // As the object collides get the tag of the object with which collision happens
        // Debug.Log("Hello");
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            trigger = true;
            keyPressSound = true;
            //  Debug.Log("We have collided");

            // Make the respective sign visible on the post
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_Color", new Color(153 / 255f, 28 / 255f, 28 / 255f));
            other.gameObject.transform.GetChild(2).GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(153 / 255f, 28 / 255f, 28 / 255f));

            currentTrigger = other.gameObject;
            objectTag = other.gameObject.tag;

            //trying
            parentTag = currentTrigger.transform.parent.gameObject.tag;
            //Debug.Log(parentTag);
            if (objectTag == "xsign")
            {
                nextTrigger = true;
                previousParentTag = currentTrigger.transform.parent.gameObject.tag;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // As the collision trigger is exited reset everything
        if (other.gameObject.tag.Equals("xsign") || other.gameObject.tag.Equals("osign"))
        {
            /*if (keyPressedOSign == false && objectTag == "osign")
            {
            //    Debug.Log("FALSE NEGATIVE");
                dataValues.falseNegative = dataValues.falseNegative + 1;
            }
            if (keyPressedXSign == false && objectTag == "xsign")
            {
                dataValues.trueNegative = dataValues.trueNegative + 1;
            }*/
            trigger = false;
            keyPressSound = true;

            // Make the sign on the collided post invisible 
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);

            keyPressedXSign = false;
            keyPressedOSign = false;
            // Debug.Log(" no collision");


            //trying...
            if (objectTag == "osign")
            {
                nextTrigger = false;
                previousParentTag = currentTrigger.transform.parent.gameObject.tag;
            }
        }
    }
}