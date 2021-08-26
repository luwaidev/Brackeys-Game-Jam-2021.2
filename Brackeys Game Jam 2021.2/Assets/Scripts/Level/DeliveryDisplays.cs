using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryDisplays : MonoBehaviour
{
    [Header("References")]
    public GameObject[] icons;
    public DoorObject[] doors;

    [Header("Positions")]
    public Vector2 idlePosition;
    public float activePosition;
    public DoorObject[] activeDeliveries;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
