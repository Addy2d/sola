using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;  // Reference to the player's Transform
    public GameObject ship;  // Reference to the player's Transform
    public Vector3 followTarget;

    public float smoothSpeed = 0.125f;  // Smoothness of the camera movement
    public Vector3 offset;  // Offset between the camera and player
    public int cameraZ = -1;

    public bool cameraSwitchTarget = false;

    void Update()
    {

        if(player.activeSelf == true)
        {
            cameraSwitchTarget = true;
        } else
        {
            cameraSwitchTarget = false;
        }

        if(cameraSwitchTarget == false)
        {
            // Target position with offset
            followTarget = ship.transform.position + offset;
            
        } else
        {
            // Target position with offset
            followTarget = player.transform.position + offset;
        }

        // Smoothly interpolate to the target position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, followTarget, smoothSpeed);
        transform.position = smoothedPosition;

        // Ensure the camera stays at the same z-axis
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraZ);
    }

}
