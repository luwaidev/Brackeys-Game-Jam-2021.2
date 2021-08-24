using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the objects that move around that reperesent the different control buttons

public class ControlObjects : MonoBehaviour
{
    public LayerMask controlLayer;
    public ControlManager cm;
    public KeyCode key;
    public bool locked;
    public string input;

    [Header("Object dragging and mouse settings")]
    public float snappingDistance;
    public float mouseRadius;
    public bool mouseSelected;

    [Header("Idle and floating settings")]
    public Vector2 floatDirection;
    public bool timerStarted;

    // Update is called once per frame
    void Update()
    {   
        // Check if the object is dropped from the mouse
        if (mouseSelected && Input.GetMouseButtonUp(0)){
            foreach (Transform point in cm.points){
                if (Vector2.Distance(transform.position, point.position) < snappingDistance)
                {
                    transform.position = point.position;
                    locked = true;
                    input = point.name;
                    break;
                } 
            }
        }
        mouseSelected = MouseOnObject();

        if (MouseOnObject()) MouseDragged(); // When the object is being dragged by the mouse
        else if (!locked && !MouseOnObject()) Floating(); // When the object is floating
    }

    bool MouseOnObject(){
        // Get mouse input
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Check if mouse might be in range
        if (Vector2.Distance(transform.position, mousePosition) < mouseRadius)
        {
            //  Raycast if mouse on position
            RaycastHit2D ray = Physics2D.Raycast(mousePosition, Vector2.up, 0.01f, controlLayer);
            return ray && ray.collider.gameObject == gameObject && Input.GetMouseButton(0);
        }
        else return false;
    }
    void Floating()
    {

    }

    void MouseDragged()
    {
        // Get mouse input
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        locked = false;
        transform.position = mousePosition;
    }

    void Locked(){
        if (!timerStarted) StartCoroutine(FloatTimer());
    }

    IEnumerator FloatTimer(){
        yield return new WaitForSeconds(Random.Range(cm.timerMinTime, cm.timerMaxTime));

        locked = false;
    }   

}
