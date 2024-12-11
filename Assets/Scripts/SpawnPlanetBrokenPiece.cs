using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class SpawnPlanetBrokenPiece : MonoBehaviour
{
    public GameObject projectilePrefab;  // Assign the prefab for the new objects in the Inspector or dynamically
    public int numberOfProjectiles = 3;  // Number of objects to spawn
    public float projectileSpeed = 5f;   // Speed at which the projectiles move

    public void Start()
    {
        projectilePrefab = GameObject.Find("BrokenPlanetPiece");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlanetPiece"))
        {
            // Calculate the direction from the collision point to the center of the game object
            Vector2 collisionDirection = (collision.transform.position - transform.position).normalized;
            Vector2 oppositeDirection = -collisionDirection;  // Opposite direction

            SpawnProjectiles(oppositeDirection);
            Destroy(gameObject);  // Destroy the current object (digger)
        }
    }

    private void SpawnProjectiles(Vector2 direction)
    {
        float angleStep = 360f / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Calculate the rotation angle for each projectile
            float angle = angleStep * i;
            Vector2 rotatedDirection = RotateVector(direction, angle);

            // Instantiate the projectile and set its velocity
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = rotatedDirection * projectileSpeed;
            }
        }
    }

    private Vector2 RotateVector(Vector2 v, float angleDegrees)
    {
        float radians = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
    }

}
