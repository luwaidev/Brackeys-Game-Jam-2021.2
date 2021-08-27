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
    public Vector2 activePosition;
    public List<DoorObject> activeDeliveries;
    public float margins;
    public float slideSpeed;

    

    // Update is called once per frame
    void Update()
    {
        ManageIcons();
    }

    void ManageIcons()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (!doors[i].active)
            {   
                if (activeDeliveries.Find(door => door.GetInstanceID() == doors[i].GetInstanceID())) activeDeliveries.Remove(doors[i]);

                // If the icon active, slide up out of view and then set to idle position
                Vector2 position = new Vector2(icons[i].transform.position.x, idlePosition.y);
                if (icons[i].transform.position.y < idlePosition.y-0.25f){
                    icons[i].transform.position = Vector2.Lerp(icons[i].transform.position, position, slideSpeed);
                }   else 
                {
                    icons[i].transform.position = idlePosition;
                }
            }
            else 
            {
                
                if (!activeDeliveries.Find(door => door.GetInstanceID() == doors[i].GetInstanceID())) activeDeliveries.Add(doors[i]);

                Vector2 position = new Vector2(activePosition.x + activeDeliveries.FindIndex(door => door.GetInstanceID() == doors[i].GetInstanceID())*margins,activePosition.y);

                // If the icon is not already active, set x position and slide into y, if not, slide towards new x position
                if (icons[i].transform.position.y > idlePosition.y-0.25f)
                {
                    icons[i].transform.position = new Vector2(position.x, icons[i].transform.position.y);
                }

                // Slide to position
                icons[i].transform.position = Vector2.Lerp(icons[i].transform.position, position, slideSpeed);
            }
        }
    
    
    }

        
}
