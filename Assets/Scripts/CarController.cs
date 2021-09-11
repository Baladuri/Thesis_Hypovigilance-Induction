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
    public List<GameObject> steeringWheels; // Wheel Colliders
    public List<GameObject> meshes; // Wheel meshes
    public float strengthCoefficient = 10000f;
    public float maxturn = 5f; // Maximum turn in wheels
    public Transform CM; // Center of Mass
    public Rigidbody rb;
    private float steerWheelsRotation = 0f;
    public GameObject steeringWheel;
    public float forwardForce = 800f; // for acceleration
    public Collider rbCollider;

    void Start()
    {
        input = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        // Manages center of mass of the truck to prevent rolling
        if (CM)
        {
            rb.centerOfMass = CM.localPosition ;
        }
        FindObjectOfType<AudioManager>().play("engine");
       
    }

    private void Update()
    {
        // For toggling headlights on and off
        if (input.l) {
            lightning.toggleHeadLights();
        }
        
    }

    void FixedUpdate()
    {
        // For implementing the constant acceleration in the truck
        rb.velocity = transform.forward * Time.fixedDeltaTime * forwardForce;

        // For implementing the steering functionalities to the wheel colliders and as well appending the rotation transformations to the steering wheel gameobject
        steeringWheel.GetComponent<Renderer>().transform.localEulerAngles = new Vector3(-69.10001f, 0f, (input.steer * maxturn));
        foreach (GameObject wheels in steeringWheels) {
            wheels.GetComponent<WheelCollider>().steerAngle = maxturn * input.steer;

            // As the steering wheels were rotating 90 degrees when starting the gameplay, had to fix their rotation separately at the beginning
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

        // For making the wheel gameobjects within the empty gameobjects(FL, FR, BL, BR) move and rotate along with the wheel colliders
        foreach (GameObject mesh in meshes) {
            mesh.transform.Rotate(mesh.transform.right * rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.35f), 0f, 0f);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("sideObject"))
        {
            rbCollider.material.dynamicFriction = 2;
            rbCollider.material.staticFriction = 2;
            /*Debug.Log("Collision with sideobject");
            rb.velocity = new Vector3(0, 0, 0);
            Debug.Log(rb.velocity);
            //rb.velocity = -rb.velocity;
            //rb.velocity = transform.forward * Time.deltaTime * 0;
            //rb.AddForce(-rb.velocity * Time.deltaTime);
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * 1000f);
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("sideObject"))
        {
            rbCollider.material.dynamicFriction = 2;
            rbCollider.material.staticFriction = 2;
            /*Debug.Log("Collision with sideobject");
            Vector3 dir = transform.TransformDirection(rb.velocity); 
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Debug.Log(rb.velocity);
            rb.Sleep();
            //rb.velocity = -rb.velocity;
            //rb.velocity = transform.forward * Time.deltaTime * 0;
            //rb.AddForce(-rb.velocity * Time.deltaTime);
            //Vector3 dir = other.contacts[0].point - transform.position;
            //dir = -dir.normalized;
            //rb.AddForce(dir * 1000f);*/
        }
    }
}
