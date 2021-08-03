using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject focus;
    
    // Distance, height and dampening for outside the truck view
    public float distance = 5f;
    public float height = 2f;
    public float dampening = 1f;

    // Front Distance 'd2', height 'h2' and the side horizontal distance 'l' (to left seat) for the inside truck view
    public float d2 = 0f;
    public float h2 = 0f;
    public float l = 0f;

    private int camMode = 0;

    void Update()
    {
        // Traverse between the view modes using 'C' Key  
        if (Input.GetKeyDown(KeyCode.C)) {
            camMode = (camMode + 1) % 2;
        }

        switch (camMode) {
            // Inside the truck view
            case 1:
                transform.position = focus.transform.position + focus.transform.TransformDirection(new Vector3(l, h2, d2));
                transform.rotation = focus.transform.rotation;
                Camera.main.fieldOfView = 60f;
                break;
            // outside the truck view
            default:
                transform.position = Vector3.Lerp(transform.position, focus.transform.position + focus.transform.TransformDirection(new Vector3(0f, height, -distance)), dampening * Time.deltaTime);
                transform.LookAt(focus.transform);
                Camera.main.fieldOfView = 40f;
                break;
        }
    }
}
