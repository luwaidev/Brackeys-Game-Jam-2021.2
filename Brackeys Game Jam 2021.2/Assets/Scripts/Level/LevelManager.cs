using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Controls the level, which doors are open, where the player needs to go

public class LevelManager : MonoBehaviour
{

    [Header("References")]
    public DoorObject[] doors;
    public TMP_Text scoreText;
    public SpriteRenderer[] crosses;
    public Sprite crossActive;

    [Header("Variables")]
    public int deliveries;
    public int failures;

    [Header("Delivery variables")]
    public float timeToNewDelivery;
    public bool timerActive;
    public float timeIncreasePercentage;
    public float timeDecreasePercentage;
    public string[] text;

    [Header("Delivery time")]
    public float deliveryTime;
    public float deliveryTimeIncreasePercentage;
    public float deliveryTimeDecreasePercentage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = deliveries.ToString();
    }

    public void IncreaseTime(){
        timeToNewDelivery += timeToNewDelivery*timeIncreasePercentage;
        deliveryTime += deliveryTime*deliveryTimeIncreasePercentage;
        failures += 1;
        if (failures <= 3) crosses[failures-1].sprite = crossActive;
    }
    public void DecreaseTime(){
        timeToNewDelivery -= timeToNewDelivery*timeDecreasePercentage;
        deliveryTime -= deliveryTime*deliveryTimeDecreasePercentage;
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
                doors[index].maxDeliveryTime = deliveryTime;
                doorAvailable = false;
                doors[index].requiredCandy = Random.Range(0,2);
                break;
            }
            
        }
        StartCoroutine(Timer());
        timerActive = false;
    }
}
