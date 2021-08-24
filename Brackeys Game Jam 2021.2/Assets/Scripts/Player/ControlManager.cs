using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check where control objects are and assign player control keys

public class ControlManager : MonoBehaviour
{
    [Header("References")]
    public Transform[] points; // List of points that represent controls
    public ControlObjects[] inputs;

    [Header("Control Object Settings")]
    public float floatingSpeed;
    public float timerMinTime;
    public float timerMaxTime;


    // Update is called once per frame
    void Update()
    {
        
    }
}
