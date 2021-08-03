using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Steering value
    public float steer;

    // Headlight value
    public bool l;
    
    void Update()
    {
        steer = Input.GetAxis("Horizontal");
        l = Input.GetKeyDown(KeyCode.L);
    }
}
