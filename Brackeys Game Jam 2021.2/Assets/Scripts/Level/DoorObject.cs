using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [Header("References")]
    private LevelManager lm;

    [Header("Variables")]
    public bool playerInRange;
    public bool active;
    public float timeActive;

    public float maxDeliveryTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F)){
            active = false;
            timeActive = 0;
            lm.DecreaseTime();
        }

        if (active) 
        {
            timeActive += Time.deltaTime; 
            
            if (timeActive > maxDeliveryTime)
            {
                active = false;
                lm.IncreaseTime();
            }
        }
        
    }
    public void SetActive(){
        active = true;
        timeActive = 0;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        playerInRange = other.tag == "Player";
    }
    private void OnTriggerExit2D(Collider2D other) {
        playerInRange = other.tag == "Player";
    }
}
