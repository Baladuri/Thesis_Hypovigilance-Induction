using UnityEngine;
using Tobii.Research;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class EyeHandler : MonoBehaviour
{
    private IEyeTracker eyeTracker;

    private Vector3 currentPosition;

    SerializePupilData _pupilData;

    public List<string> _pupilDataList = new List<string>();

    private float stopWatch;
    public string currentTime;
    public bool taskTimeControl = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        Utility.getFirstEyeTracker(
            handle: (eyetracker) => this.eyeTracker = eyetracker
        );
    }
    void Start()
    {
        try
        {
            eyeTracker.GazeDataReceived += GazeDataReceivedFromTracker;
        }
        catch { }
        _pupilData = new SerializePupilData();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentPosition != null)
        {
            transform.position = new Vector2(currentPosition.x, currentPosition.y);
        }*/
        if (taskTimeControl == true)
        {
            stopWatch = stopWatch + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(stopWatch);
            currentTime = time.ToString(@"mm\:ss\:fff");
        }
        Debug.Log(currentTime);
        string pupilJson = JsonUtility.ToJson(_pupilData, true).ToString();
        _pupilDataList.Add(pupilJson);
    }

    void OnDestroy()
    {
        try
        {
            eyeTracker.GazeDataReceived -= GazeDataReceivedFromTracker;
        }
        catch { }
        Debug.Log("Saving Pupil data for " + SceneManager.GetActiveScene().name+"....");
        foreach (string json in _pupilDataList)
        {
            SavePupilDataManager.AppendToFile(json, SceneManager.GetActiveScene().name);
        }
    }

    private void GazeDataReceivedFromTracker(object sender, GazeDataEventArgs e)
    {
        if (e.LeftEye.Pupil.Validity == Validity.Invalid && e.RightEye.Pupil.Validity == Validity.Invalid)
        {
            _pupilData.both_pupils_invalid = (e.LeftEye.Pupil.PupilDiameter + e.RightEye.Pupil.PupilDiameter) / 2;
        }
        else if (e.LeftEye.Pupil.Validity == Validity.Valid && e.RightEye.Pupil.Validity == Validity.Invalid)
        {
            _pupilData.left_pupil_valid = e.LeftEye.Pupil.PupilDiameter;
        }
        else if (e.LeftEye.Pupil.Validity == Validity.Invalid && e.RightEye.Pupil.Validity == Validity.Valid) 
        {
            _pupilData.right_pupil_valid = e.RightEye.Pupil.PupilDiameter;
        }
        else if (e.LeftEye.Pupil.Validity == Validity.Valid && e.RightEye.Pupil.Validity == Validity.Valid)   
        {
            _pupilData.both_pupils_valid = (e.LeftEye.Pupil.PupilDiameter + e.RightEye.Pupil.PupilDiameter) / 2;
        }
        _pupilData.time_stamp = System.DateTime.UtcNow.ToString();
        _pupilData.relative_time = currentTime;


        // If There is no valid eye gaze data, stop the function
        /*if (e.LeftEye.GazePoint.Validity == Validity.Invalid ||
            e.RightEye.GazePoint.Validity == Validity.Invalid)
        {
            return;
        }

        // get the average of 2 eyes gazes data at each momment
        var combinedEyeGazePoint = (
            Utility.ToVector2(e.LeftEye.GazePoint.PositionOnDisplayArea) +
            Utility.ToVector2(e.RightEye.GazePoint.PositionOnDisplayArea)
        ) / 2f;

        // translate to scene's coordinate system to get the display gaze point on screen
        var position = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Screen.width * combinedEyeGazePoint.x,
                Screen.height * (1 - combinedEyeGazePoint.y),
                10)
        );

        currentPosition = position;*/
    }
    public void taskStopWatch()
    {
        taskTimeControl = true;
    }
}