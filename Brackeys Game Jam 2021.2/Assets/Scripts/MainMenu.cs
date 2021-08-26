using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    public GameManager gm;

    private void Start() {
        gm = GameObject.FindGameObjectWithTag("GameManger").GetComponent<GameManager>();
    }

    public void Play(){
        gm.Play();
    }
}
