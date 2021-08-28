using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryDisplays : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer[] icons;
    public DoorObject[] doors;

    [Header("Positions")]
    public Vector2 idlePosition;
    public Vector2 activePosition;
    public List<DoorObject> activeDeliveries;
    public float margins;
    public float slideSpeed;
    public float defaultSliderLength;

    

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
                Vector2 position = new Vector2(icons[i].transform.localPosition.x, idlePosition.y);
                if (icons[i].transform.localPosition.y < idlePosition.y-0.25f){
                    icons[i].transform.localPosition = Vector2.Lerp(icons[i].transform.localPosition, position, slideSpeed);
                }   else 
                {
                    icons[i].transform.localPosition = idlePosition;
                }
            }
            else 
            {
                
                if (!activeDeliveries.Find(door => door.GetInstanceID() == doors[i].GetInstanceID())) activeDeliveries.Add(doors[i]);

                Vector2 position = new Vector2(activePosition.x + activeDeliveries.FindIndex(door => door.GetInstanceID() == doors[i].GetInstanceID())*margins,activePosition.y);

                // If the icon is not already active, set x position and slide into y, if not, slide towards new x position
                if (icons[i].transform.localPosition.y > idlePosition.y-0.25f)
                {
                    icons[i].transform.localPosition = new Vector2(position.x, icons[i].transform.localPosition.y);
                }

                icons[i].transform.GetChild(0).localScale = new Vector2(defaultSliderLength-defaultSliderLength*doors[i].timeActive/doors[i].maxDeliveryTime ,icons[i].transform.GetChild(0).localScale.y);

                // Slide to position
                icons[i].transform.localPosition = Vector2.Lerp(icons[i].transform.localPosition, position, slideSpeed);
                if (doors[i].requiredCandy == 0) icons[i].sprite = doors[i].redSprite;
                else if (doors[i].requiredCandy == 1) icons[i].sprite = doors[i].blueSprite;
                else if (doors[i].requiredCandy == 2) icons[i].sprite = doors[i].greenSprite;
            }
        }
    
    
    }

        
}
