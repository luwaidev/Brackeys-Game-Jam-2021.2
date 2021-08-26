using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the level, which doors are open, where the player needs to go

public class LevelManager : MonoBehaviour
{

    [Header("References")]
    public DoorObject[] doors;

    [Header("Variables")]
    public float deliveries;
    public int failures;
    public float timeToNewDelivery;
    public bool timerActive;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
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
            int index = Random.Range(0, doors.Length);
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
