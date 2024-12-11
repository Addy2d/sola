using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutoDiggerAction : MonoBehaviour
{
    public GameObject autoDiggerPrefab;       // Prefab for the digger

    public Transform[] planets;           // Array to hold multiple planets
    public float detectionRadius = 5f;    // Distance to detect nearby planets
    public float moveSpeed = 5f;          // Speed at which the digger moves

    private bool isMoving = false;        // Track if the digger is moving
    private Transform target;             // Target reference for movement
    private Transform currentPlanet;      // Currently assigned planet

    public float startDelay = 1f; // Time before the first execution
    public float interval = 2f; // Time between subsequent executions

    private void Start()
    {
        InvokeRepeating("ExecuteAction", startDelay, interval);

        // Check for the nearest planet
        CheckForClosestPlanet();
    }

    void ExecuteAction()
    {
        
            if (autoDiggerPrefab != null)
            {
                SpawnDigger();
            }
    }

    private void Update()
    {
        // Move if active
        if (isMoving && target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void SpawnDigger()
    {
        GameObject digger = Instantiate(autoDiggerPrefab, transform.position, Quaternion.identity);
        SimpleDigger diggerScript = digger.AddComponent<SimpleDigger>();

        diggerScript.Initialize(moveSpeed, currentPlanet);
    }

    public void Initialize(float speed, Transform targetTransform)
    {
        moveSpeed = speed;
        target = targetTransform;
        isMoving = true;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            moveSpeed * Time.deltaTime
        );

        // Rotate to face the target
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void CheckForClosestPlanet()
    {
        if (planets == null || planets.Length == 0)
        {
            // Debug.LogWarning("No planets assigned in the Inspector!");
            return;
        }

        Transform closestPlanet = null;
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

        if (closestPlanet != currentPlanet)
        {
            currentPlanet = closestPlanet;
            if (currentPlanet != null)
            {
                // Debug.Log("Closest planet: " + currentPlanet.name);
            }
        }
    }

    // Function to change the radius of the CircleCollider2D by a specified amount
    public void ChangeRadius()
    {
        // Get the CircleCollider2D component attached to this GameObject
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();

        // Adjust the radius by the specified amount
        circleCollider.radius += 1;

        // Optional: Clamp the radius to ensure it doesn't go below zero
        circleCollider.radius = Mathf.Max(0, circleCollider.radius);
    }
}
