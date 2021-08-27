using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("References")]
    public DoorObject door;
    private GameObject player;
    private SpriteRenderer sr;
    public LayerMask arrowLayer;

    [Header("Variables")]
    public float distance;
    public float raycastDistance;
    public float speed;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
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
                if (obj.collider.gameObject == door.gameObject) doorPosition = obj.point;
                else if (obj.collider.tag == "CameraWall") cameraWallPosition = obj.point;
            }

            Vector2 direction = (door.transform.position-player.transform.position).normalized;

            if (doorPosition == Vector2.zero)
            {
                transform.position = Vector2.Lerp(transform.position, cameraWallPosition-(direction*distance), speed);
            }   else 
            {
                transform.position = Vector2.Lerp(transform.position, doorPosition-(direction*distance), speed);
            }

            transform.eulerAngles = new Vector3( 0, 0, Mathf.Atan2(direction.y, direction.x)*180/Mathf.PI);
            
        }   else 
        {
            sr.enabled = false;
            transform.position = player.transform.position;
        }
    }
}
