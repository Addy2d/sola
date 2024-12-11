using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 180.0f;
    public float movementDrag = 0.5f;
    public float rotationDrag = 0.5f;
    public float brakingSpeed = 5.0f;
    public float reverseSpeed = 2.0f;

    private float angularVelocity;
    private float movementVelocity;
    private Rigidbody2D rb;

    // public GameObject objectToIgnore; // Reference to the game object whose collider should be ignored

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called a fixed number of times per second
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Apply movement drag to slow down the spaceship's movement over time
        movementVelocity *= Mathf.Clamp01(1.0f - movementDrag * Time.fixedDeltaTime);

        // Apply rotation drag to slow down the spaceship's rotation over time
        angularVelocity *= Mathf.Clamp01(1.0f - rotationDrag * Time.fixedDeltaTime);

        // Apply forward movement to the spaceship
        float forwardMovementSpeed = Mathf.Clamp(speed * verticalInput, 0.0f, speed);
        movementVelocity += forwardMovementSpeed * Time.fixedDeltaTime;

        // Apply braking force to slow down the spaceship
        if (verticalInput < 0.0f)
        {
            movementVelocity -= brakingSpeed * Time.fixedDeltaTime;
            movementVelocity = Mathf.Max(movementVelocity, -speed);
        }

        // Apply reverse movement to the spaceship
        if (verticalInput < 0.0f && movementVelocity < 0.0f)
        {
            movementVelocity -= reverseSpeed * Time.fixedDeltaTime;
            movementVelocity = Mathf.Min(movementVelocity, -reverseSpeed);
        }

        // Apply movement to the spaceship
        Vector2 direction = transform.up;
        rb.MovePosition(rb.position + direction * movementVelocity * Time.fixedDeltaTime);

        // Apply rotation to the spaceship
        float rotation = -horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        angularVelocity += rotation;
        rb.MoveRotation(rb.rotation + angularVelocity * Time.fixedDeltaTime);
    }

}
