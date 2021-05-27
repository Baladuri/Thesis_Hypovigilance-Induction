using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviouralData : MonoBehaviour
{
    public int truePositive;
    public int falsePositive;
    public int trueNegative;
    public int falseNegative;
  
    void Update()
    {
        Debug.Log("True Positives: " + truePositive);
        Debug.Log("False Positives: " + falsePositive);
        Debug.Log("True Negatives: " + trueNegative);
        Debug.Log("False Negatives: " + falseNegative);
    }
}
