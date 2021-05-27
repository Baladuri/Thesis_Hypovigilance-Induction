using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightningManager))]
public class CarController : MonoBehaviour
{                  
    public InputManager input;
    public LightningManager lightning;
    public UIManager UI;
    //public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheels;
    public List<GameObject> meshes;
    public float strengthCoefficient = 10000f;
    public float maxturn = 5f;
    public Transform CM;
    public Rigidbody rb;
    private float steerWheelsRotation = 0f;
    public float steerRotation;
    public float brakeStrength;
    //public GameObject backLights;
    public List<GameObject> backLights;
    public GameObject steeringWheel;
    public float forwardForce = 20000f;

   
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();


        if (CM)
        {
            rb.centerOfMass = CM.localPosition ;
        }
        FindObjectOfType<AudioManager>().play("engine");
       
    }

    private void Update()
    {
        if (input.l) {
            lightning.toggleHeadLights();
        }

        //backLights.GetComponent<Renderer>().material.SetColor("_EmissionColor", input.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        /*foreach (GameObject backLight in backLights)
        {           
            backLight.GetComponent<Renderer>().material.SetColor("_EmissionColor", input.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
        }

        UI.changeText(transform.InverseTransformVector(rb.velocity).z);*/
       
    }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(forwardForce * Time.deltaTime,0,0);
        rb.velocity = transform.forward * Time.deltaTime * forwardForce;
        /*foreach(WheelCollider wheels in throttleWheels){
            
            if (input.brake)
            {
                wheels.motorTorque = 0f;
                wheels.brakeTorque = brakeStrength * Time.deltaTime;
            }
            else 
            {
                wheels.motorTorque = strengthCoefficient * Time.deltaTime * input.throttle;
                wheels.brakeTorque = 0f;
            }

        }*/
        steeringWheel.GetComponent<Renderer>().transform.localEulerAngles = new Vector3(-69.10001f, 0f, steerRotation + (input.steer * maxturn));
        foreach (GameObject wheels in steeringWheels) {
            wheels.GetComponent<WheelCollider>().steerAngle = maxturn * input.steer;
            if (wheels.name == "FL")
            {
                steerWheelsRotation = -90f;
            }
            else if (wheels.name == "FR")
            {
                steerWheelsRotation = 90f;
            }
            wheels.transform.localEulerAngles = new Vector3(0f, steerWheelsRotation + (input.steer * maxturn), 0f);
            
            
        }

        foreach (GameObject mesh in meshes) {
            mesh.transform.Rotate(mesh.transform.right * rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.35f), 0f, 0f);
        }
    }
}
