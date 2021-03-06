using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorObject : MonoBehaviour
{
    [Header("References")]
    private LevelManager lm;
    private PlayerController player;
    public TMP_Text textBox;
    public bool disabled;
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite greenSprite;

    [Header("Variables")]
    public bool playerInRange;
    public bool active;
    public float timeActive;
    public int requiredCandy;
    public float maxDeliveryTime;
    public float textTime;
    private bool playing;
    // Start is called before the first frame update
    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled){
            bool hasCandy = (requiredCandy == 0 && player.hasRedCandy) || (requiredCandy == 1 && player.hasBlueCandy) || (requiredCandy == 2 && player.hasGreenCandy);
            if (playerInRange && Input.GetKeyDown(KeyCode.F) && hasCandy){
                active = false;
                timeActive = 0;
                lm.DecreaseTime();
                if (requiredCandy == 0) player.hasRedCandy = false;
                if (requiredCandy == 1) player.hasBlueCandy = false;
                if (requiredCandy == 2) player.hasGreenCandy = false;
                StartCoroutine(Timer());
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
        } else 
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.F)){
                if (!playing) StartCoroutine(AnimationTimer());
            }
        }
        
    }
    public void SetActive(){
        active = true;
        timeActive = 0;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") playerInRange = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") playerInRange = false;
    }

    IEnumerator Timer(){
        textBox.text = lm.text[Random.Range(0, lm.text.Length)];
        yield return new WaitForSeconds(textTime);
        textBox.text = "";
    }

    IEnumerator AnimationTimer(){
        playing = true;
        active = false;
        yield return new WaitForSeconds(1f);
        active = true;
        playing = false;
    }
}
