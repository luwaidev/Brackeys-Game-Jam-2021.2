using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("References")]
    public DoorObject door;
    private GameObject player;
    private SpriteRenderer sr;
    private Animator anim;
    public LayerMask arrowLayer;

    [Header("Variables")]
    public float distance;
    public float raycastDistance;
    public float speed;
    public float snappingDistance;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (door.active)
        {
            sr.enabled = true;
            RaycastHit2D[] ray = Physics2D.RaycastAll(player.transform.position, door.transform.position-player.transform.position, raycastDistance, arrowLayer);
            
            Vector2 doorPosition = Vector2.zero;
            Vector2 cameraWallPosition = Vector2.zero;
            foreach (RaycastHit2D obj in ray){
                if (obj.collider.gameObject == door.gameObject) 
                {
                    doorPosition = obj.point;
                    break;
                }
                else if (obj.collider.tag == "CameraWall") 
                {
                    cameraWallPosition = obj.point;
                    break;
                }
                 
            }


            Vector2 direction = (door.transform.position-player.transform.position).normalized;
            
            if (Vector2.Distance(player.transform.position, door.transform.position) < snappingDistance)
            {
                transform.position = Vector2.Lerp(transform.position, door.transform.position, speed);
            }
            else if (doorPosition == Vector2.zero)
            {
                transform.position = Vector2.Lerp(transform.position, cameraWallPosition-(direction*distance), speed);
            }   else 
            {
                transform.position = Vector2.Lerp(transform.position, doorPosition-(direction*distance), speed);
            }

            if (Vector2.Distance(transform.position, door.transform.position)<distance+1) anim.SetTrigger("Point");
            anim.SetBool("Active", true);
            transform.eulerAngles = new Vector3( 0, 0, Mathf.Atan2(direction.y, direction.x)*180/Mathf.PI);
            
        }   else 
        {
            anim.SetBool("Active", false);
            transform.position = door.transform.position;
            transform.eulerAngles = new Vector3( 0, 0, 270);
        }
    }
}
