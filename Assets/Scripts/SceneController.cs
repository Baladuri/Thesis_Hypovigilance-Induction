using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject truck;
    public GameObject returnToMainMenuPanel;
    BehaviouralData saveData;
    
   // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }
    public void changeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit()
    {
        Application.Quit();
    }

    public void startHypovigilance() 
    {
        StartCoroutine(HypoVigilTaskDuration());
    }
    public void startHypervigilance()
    {
        StartCoroutine(HyperVigilTaskDuration());
    }
    public void startControlCondition()
    {
        StartCoroutine(ControlConditionTaskDuration());
    }

    IEnumerator HypoVigilTaskDuration() {
        yield return new WaitForSeconds(1000);
        BehaviouralData.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);    
    }

    IEnumerator HyperVigilTaskDuration() {
        yield return new WaitForSeconds(50);
        BehaviouralData.SaveData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ControlConditionTaskDuration() {
        yield return new WaitForSeconds(10);
        returnToMainMenuPanel.gameObject.SetActive(true);
    }

    public void goBackToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        truck.gameObject.SetActive(false);
    }

}
