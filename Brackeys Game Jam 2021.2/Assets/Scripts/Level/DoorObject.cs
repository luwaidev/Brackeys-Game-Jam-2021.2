using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : MonoBehaviour
{
    [Header("References")]
    private LevelManager lm;
    private PlayerController player;

    [Header("Variables")]
    public bool playerInRange;
    public bool active;
    public float timeActive;
    public int requiredCandy;
    public float maxDeliveryTime;
    // Start is called before the first frame update
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool hasCandy = (requiredCandy == 0 && player.hasRedCandy) || (requiredCandy == 1 && player.hasBlueCandy) || (requiredCandy == 2 && player.hasGreenCandy);
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && hasCandy){
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
        playerInRange = !(other.tag == "Player");
    }
}
