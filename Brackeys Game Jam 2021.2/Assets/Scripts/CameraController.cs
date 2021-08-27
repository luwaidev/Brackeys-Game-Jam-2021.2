using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public GameObject target;
    

    [Header("Camera Settings")]
    public float cameraSize;
    public float cameraSpeed;
    public Vector3 offset;
    public Vector2 maxPositions;
    public Vector2 minPositions;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set the target to the player
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Like update, but is always run at the same time with the physics, should prevent camera stuttering
    void FixedUpdate()
    {
        followTarget();
    }

    void followTarget(){
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");

        // Set the intended position
        Vector3 targetPosition = target.transform.position + offset;

        if (targetPosition.x > maxPositions.x) targetPosition.x = maxPositions.x;
        else if (targetPosition.x < minPositions.x) targetPosition.x = minPositions.x;

        if (targetPosition.y > maxPositions.y) targetPosition.y = maxPositions.y;
        else if (targetPosition.y < minPositions.y) targetPosition.y = minPositions.y;

        // Lerp the camera towards the position
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed);
    }
}
