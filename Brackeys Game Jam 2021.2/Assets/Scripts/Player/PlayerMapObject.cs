using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapObject : MonoBehaviour
{
    private Transform player;

    [Header("Setting")]
    public Vector3 zero;
    public float scale;
    

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = player.position*scale + zero;
    }
}
