using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the objects that move around that reperesent the different control buttons

public class ControlObjects : MonoBehaviour
{
    [Header("References")]
    public LayerMask controlLayer;
    private ControlManager cm;
    private Rigidbody2D rb;
    private Animator anim;
    public KeyCode key;

    [Header("Info")]
    public bool locked;
    public string input;

    [Header("Object dragging and mouse settings")]
    public float snappingDistance;
    public float mouseRadius;
    public bool mouseSelected;

    [Header("Idle and floating settings")]
    public Vector2 floatDirection;
    private Vector2 lastPosition;
    public float changePositionDistance;
    public bool timerStarted;

    private void Awake() {
        cm = GameObject.FindGameObjectWithTag("ControlManager").GetComponent<ControlManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {   

        // Check if the object is dropped from the mouse
        bool canLock = false;
        if (mouseSelected && Input.GetMouseButtonUp(0)){
            foreach (Transform point in cm.points){
                foreach (ControlObjects obj in cm.inputs){
                    if (obj != this && obj.locked && obj.input == point.name)
                    {
                        canLock = false;
                        input = "";
                        break;
                    }
                    if (Vector2.Distance(transform.position, point.position) < snappingDistance)
                    {
                        canLock = true;
                    } 
                }

                if (canLock)
                {
                    transform.position = point.position;
                    locked = true;
                    input = point.name;
                    break;
                }
                
            }
            cm.SetPlayerControls();
            
        }
        
        if (MouseOnObject()) mouseSelected = true;
        if (Input.GetMouseButtonUp(0)) mouseSelected = false;

        if (mouseSelected && !locked) MouseDragged(); // When the object is being dragged by the mouse
        else if (!locked && !MouseOnObject()) Floating(); // When the object is floating
        else if (locked) Locked();

    }

    bool MouseOnObject(){
        // Get mouse input
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;

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
        if (floatDirection == Vector2.zero || Vector2.Distance(transform.position, lastPosition) > changePositionDistance) 
        {
            floatDirection = new Vector2(Random.Range(-100,100)/100f, Random.Range(-100,100)/100f);
            lastPosition = transform.position;
        }
        
        rb.velocity = floatDirection*cm.floatingSpeed;
        anim.SetBool("Floating", true);
    }

    void MouseDragged()
    {
        // Get mouse input
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        locked = false;
        transform.position = mousePosition;
        input = "";
        StopAllCoroutines();
        anim.SetTrigger("Stop");
        cm.SetPlayerControls();

        anim.SetBool("Floating", true);
    }

    void Locked(){
        floatDirection = Vector2.zero;
        rb.velocity = Vector2.zero;
        if (!timerStarted) StartCoroutine(FloatTimer());
        anim.SetBool("Floating", false);
    }

    IEnumerator FloatTimer(){
        timerStarted = true;
        float time = Random.Range(cm.timerMinTime, cm.timerMaxTime);

        yield return new WaitForSeconds(time - 5);

        anim.SetTrigger("Shake");

        yield return new WaitForSeconds(5);

        timerStarted = false;
        input = "";
        anim.SetTrigger("Pop");
        locked = false; 
        cm.SetPlayerControls();
    }   

}
