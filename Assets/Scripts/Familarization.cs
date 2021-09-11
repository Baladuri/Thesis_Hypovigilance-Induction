using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Familarization : MonoBehaviour
{
    public GameObject changeCameraViewPanel;
    public GameObject respondToTheSignPostPanel;
    public GameObject steeringControlPanel;
    public GameObject keepCloseToMiddleMarkerPanel;
    public GameObject testingOSign;
    private bool triggerOSign;
    // Start is called before the first frame update
    public void startCouroutines() {
        StartCoroutine(cameraViewPanel());
    }

    // Update is called once per frame
    void Update()
    {
        if (changeCameraViewPanel.activeSelf == true) {
            if (Input.GetKeyDown(KeyCode.C))
            {
                changeCameraViewPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (triggerOSign == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                respondToTheSignPostPanel.SetActive(false);
                StartCoroutine(steeringControl());
            }
        }
    }

    IEnumerator cameraViewPanel() {
        yield return new WaitForSeconds(3f);
        changeCameraViewPanel.SetActive(true);
        Time.timeScale = 0;
    }
    IEnumerator steeringControl() {
        yield return new WaitForSeconds(5f);
        steeringControlPanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator keepCloseToMiddleMarker()
    {
        yield return new WaitForSeconds(5f);
        keepCloseToMiddleMarkerPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void closePanel() 
    {
        if (steeringControlPanel.activeSelf == true)
        {
            steeringControlPanel.SetActive(false);
            StartCoroutine(keepCloseToMiddleMarker());
        }
        else if (keepCloseToMiddleMarkerPanel.activeSelf == true)
        {
            keepCloseToMiddleMarkerPanel.SetActive(false);
        }       
        Time.timeScale = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.gameObject.Equals(testingOSign)) {
            respondToTheSignPostPanel.SetActive(true);
            triggerOSign = true;
            Time.timeScale = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.gameObject.Equals(testingOSign))
        {
            triggerOSign = false;
        }
    }
}
