using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the level, which doors are open, where the player needs to go

public class LevelManager : MonoBehaviour
{

    [Header("References")]
    public DoorObject[] doors;

    [Header("Variables")]
    public int deliveries;
    public int failures;

    [Header("Delivery variables")]
    public float timeToNewDelivery;
    public bool timerActive;
    public float timeIncreasePercentage;
    public float timeDecreasePercentage;
    public string[] text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseTime(){
        timeToNewDelivery += timeToNewDelivery*timeIncreasePercentage;
        failures += 1;
    }
    public void DecreaseTime(){
        timeToNewDelivery -= timeToNewDelivery*timeDecreasePercentage;
        deliveries += 1;
    }

    IEnumerator Timer(){
        timerActive = true;
        yield return new WaitForSeconds(timeToNewDelivery);

        bool doorAvailable = false;
        foreach(DoorObject door in doors) 
        {
            if (!door.active){
                doorAvailable = true;
                break;
            }
        }

        while (doorAvailable)
        {
            int index = Random.Range(0, doors.Length-1);
            if (!doors[index].active)
            {
                doors[index].active = true;
                doorAvailable = false;
                break;
            }
            
        }
        StartCoroutine(Timer());
        timerActive = false;
    }
}
