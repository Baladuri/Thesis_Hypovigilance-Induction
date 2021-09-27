using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject truck;
    public GameObject returnToMainMenuPanel;
    public Animator FadeOut;
    public Animator FadeIn;
    public Animator FadeOutNewTaskPanel;
    public GameObject taskOverPanel;
    public Animator StartTask;
    private Familarization familarization;

    private IEnumerator Start()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        StartTask.SetTrigger("Start");
        familarization = GetComponent<Familarization>();           
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(fadeOutAnimation());
    }
    public void startFamilarizationScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(fadeOutAnimationFamilarization());
    }
    public void quit()
    {
        Application.Quit();
    }

    public void startHypovigilance() 
    {
        Time.timeScale = 1f;
        StartCoroutine(HypoVigilTaskDuration());
    }
    public void startHypervigilance()
    {
        Time.timeScale = 1f;
        StartCoroutine(HyperVigilTaskDuration());
    }
    public void startControlCondition()
    {
        Time.timeScale = 1f;
        StartCoroutine(ControlConditionTaskDuration());
    }
    public void startFamilarization()
    {
        Time.timeScale = 1f;
        StartCoroutine(FamilarizationTaskDuration());
        familarization.startCouroutines();
    }

    IEnumerator HypoVigilTaskDuration() {
        yield return new WaitForSeconds(600f);
        BehaviouralData.SaveData();
        StartCoroutine(fadeInAnimation());

    }

    IEnumerator HyperVigilTaskDuration() {
        yield return new WaitForSeconds(600f);
        BehaviouralData.SaveData();
        StartCoroutine(fadeInAnimation());
    }

    IEnumerator ControlConditionTaskDuration() {
        yield return new WaitForSeconds(60f);
        StartCoroutine(fadeInAnimation());
    }

    IEnumerator FamilarizationTaskDuration()
    {
        yield return new WaitForSeconds(20f);
        StartCoroutine(fadeInAnimation());
        returnToMainMenuPanel.SetActive(true);
    }

    IEnumerator fadeInAnimation() {
        FadeIn.SetTrigger("StartFadeIn");
        yield return new WaitForSeconds(5f);
        truck.gameObject.SetActive(false);
        taskOverPanel.gameObject.SetActive(true);
    }

    public void NextTask() {
        StartCoroutine(fadeOutAnimation());
    }

    IEnumerator fadeOutAnimation() {
        FadeOutNewTaskPanel.SetTrigger("FadeOutNewTaskPanel");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator fadeOutAnimationFamilarization()
    {
        FadeOutNewTaskPanel.SetTrigger("FadeOutNewTaskPanel");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Familarization");
    }
    public void goBackToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        //truck.gameObject.SetActive(false);
    }

}
