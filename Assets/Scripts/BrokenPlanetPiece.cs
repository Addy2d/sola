using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPlanetPiece : MonoBehaviour
{
    public float slowDownDuration = 3f;  // Time over which the object will slow down
    public float delay = 2f;  // Delay before slowing down starts

    private Rigidbody2D rb;
    private bool isSlowingDown = false;
    private float elapsedTime = 0f;
    private Vector2 initialVelocity;

    public Transform player;  // The player object to move towards

    public float moveSpeed = 5f;  // Speed at which the object moves
    public bool shouldMove = true;  // Whether the object should move towards the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke(nameof(StartSlowingDown), delay);  // Start slowing down after the delay
    }

    void Update()
    {
        if (isSlowingDown)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slowDownDuration;

            // Gradually reduce velocity
            rb.velocity = Vector2.Lerp(initialVelocity, Vector2.zero, t);

            // Stop slowing down when the duration is complete
            if (elapsedTime >= slowDownDuration)
            {
                isSlowingDown = false;
                rb.velocity = Vector2.zero;  // Ensure the object stops completely
            }
        }

        // Calculate the distance from the current object to the player
        float distance = Vector2.Distance(transform.position, player.position);

        // If shouldMove is true, move the object towards the player
        if (shouldMove)
        {
            // Get the direction from this object to the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the object towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void StartSlowingDown()
    {
        isSlowingDown = true;
        initialVelocity = rb.velocity;  // Capture the velocity at the start of the slowdown
        elapsedTime = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shouldMove = true;
        }

        if (collision.CompareTag("Collector"))
        {
            Destroy(gameObject);

            WeaponPixelDustCollectorMagnet pixelDustCollectorMagnetScript = FindObjectOfType<WeaponPixelDustCollectorMagnet>();
            pixelDustCollectorMagnetScript.IncreasePixelDustCollectedAmount();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            PixelDustCounter counterScript = FindObjectOfType<PixelDustCounter>();
            counterScript.pixelDustIncrease();
        }
    }

}
