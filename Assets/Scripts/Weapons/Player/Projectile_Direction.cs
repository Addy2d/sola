using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Direction : MonoBehaviour
{
    public Transform playerRefernce; // Reference to the center object to circle around
    public float radius = 2f; // Radius of the circular path
    public float speed = 2f; // Speed of rotation

    public Vector3 direction;
    public bool multiToolInsidePlanet;

    private void Update()
    {
        // Get the mouse position
        Vector3 mousePosition = Input.mousePosition;
        // Convert the mouse position to world space
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0f;
        // Calculate the direction from the center object to the mouse position
        direction = worldMousePosition - playerRefernce.position;
        float distance = direction.magnitude;

        // Check if the multitool game object is inside or outside the radius
        if (distance > radius)
        {
            // Move the multitool game object around the radius
            Vector3 targetPosition = playerRefernce.position + direction.normalized * radius;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            // Move the multitool game object to mouse position
            transform.position = Vector3.MoveTowards(transform.position, worldMousePosition, speed * Time.deltaTime);
        }

        // Calculate the rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the game object to face the center object
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlanetPiece")) 
        {
            multiToolInsidePlanet = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlanetPiece"))
        {
            multiToolInsidePlanet = false;
        }
    }
}
