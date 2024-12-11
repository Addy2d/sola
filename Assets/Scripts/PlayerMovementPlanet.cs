using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPlanet : MonoBehaviour
{
    public Transform planetCenterPoint;

    public Transform[] planets;           // Array to hold multiple planets
    public float detectionRadius = 5f;    // Distance to detect nearby planets

    public float gravityStrength = 9.8f; // Adjust this value to control the strength of gravity

    public float moveSpeed = 2.5f;
    public float maxSpeed = 2.5f;
    public float maxJumpSpeed = 2.5f;
    public Transform moveLeftMarker; // Assign the target GameObject in the Unity Editor
    public Transform moveRightMarker; // Assign the target GameObject in the Unity Editor

    public float jumpForce = 200f;
    private Rigidbody2D rb;

    public Transform closestPlanet;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Check for the nearest planet
        CheckForClosestPlanet();

        // Player gravity towards planet center point:
        // Calculate the direction vector towards the target object's center
        Vector2 direction = planetCenterPoint.position - transform.position;

        // Normalize the direction vector to ensure consistent movement speed
        direction.Normalize();

        // Apply gravity to the object by adding a force in the direction of the target
        GetComponent<Rigidbody2D>().AddForce(direction * gravityStrength);

        // Player rotates around planet center point:
        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object to face the target
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Player Jump: 
        if (Input.GetButtonDown("Jump") && rb.velocity.x < maxSpeed && rb.velocity.y < maxJumpSpeed)
        {
            // Jump away from the planet's center
            Vector2 jumpDirection = (transform.position - planetCenterPoint.position).normalized;
            rb.AddForce(jumpDirection * jumpForce);
        }

        // Player Movement:
        if (Input.GetKey(KeyCode.A) && rb.velocity.magnitude < maxSpeed)
        {
            Vector2 moveDirection = (moveLeftMarker.position - transform.position).normalized;
            rb.AddForce(moveDirection * moveSpeed);
            rb.constraints = RigidbodyConstraints2D.None; // Stop Slope Slide
        }


        if (Input.GetKey(KeyCode.D) && rb.velocity.magnitude < maxSpeed)
        {
            Vector2 moveDirection = (moveRightMarker.position - transform.position).normalized;
            rb.AddForce(moveDirection * moveSpeed);
            rb.constraints = RigidbodyConstraints2D.None;  // Stop Slope Slide
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            // Freeze the z-axis rotation by setting constraints
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;  // Stop Slope Slide
        }
    }

    private void CheckForClosestPlanet()
    {
        if (planets == null || planets.Length == 0)
        {
            // Debug.LogWarning("No planets assigned in the Inspector!");
            return;
        }

        closestPlanet = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform planet in planets)
        {
            if (planet == null)
            {
                // Debug.LogWarning("A planet reference is null!");
                continue;
            }

            float distance = Vector3.Distance(transform.position, planet.position);
            if (distance < closestDistance && distance <= detectionRadius)
            {
                closestDistance = distance;
                closestPlanet = planet;
            }
        }

        if (closestPlanet != planetCenterPoint)
        {
            planetCenterPoint = closestPlanet;
            if (planetCenterPoint != null)
            {
                // Debug.Log("Closest planet: " + currentPlanet.name);
            }
        }
    }

}
