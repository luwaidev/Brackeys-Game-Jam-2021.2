using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Check where control objects are and assign player control keys

public class ControlManager : MonoBehaviour
{
    [Header("References")]
    private PlayerController player;
    public Transform[] points; // List of points that represent controls
    public ControlObjects[] inputs;

    [Header("Control Object Settings")]
    public float floatingSpeed;
    public float timerMinTime;
    public float timerMaxTime;


    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerControls(){
        foreach (ControlObjects obj in inputs){
            if (obj.input == "Left") player.leftKey = obj.key;
            else if (obj.input == "Right") player.rightKey = obj.key;
            else if (obj.input == "Up") player.upKey = obj.key;
        }
    }
}
