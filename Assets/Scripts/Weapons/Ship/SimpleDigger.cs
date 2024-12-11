using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SimpleDigger : MonoBehaviour
    {
    public GameObject diggerPrefab;       // Prefab for the digger
    public Transform[] planets;           // Array to hold multiple planets
    public float detectionRadius = 5f;    // Distance to detect nearby planets
    public float moveSpeed = 5f;          // Speed at which the digger moves
    public float spawnDelay = 2f;         // Delay in seconds before next spawn
    private float lastSpawnTime = 0f;     // Time of last spawn

    private bool isMoving = false;        // Track if the digger is moving
    private Transform target;             // Target reference for movement
    private Transform currentPlanet;      // Currently assigned planet

    public bool canSpawn = true;

    private void Start()
    {
        canSpawn = true;
    }

    void Update()
    {

        if(Input.GetKey(KeyCode.Alpha1))
        {
            canSpawn = true;

        } else if (Input.GetKey(KeyCode.Alpha2))
        {
            canSpawn = false;

        } else if (Input.GetKey(KeyCode.Alpha3))
        {
            canSpawn = false;

        } else if (Input.GetKey(KeyCode.Alpha4))
        {
            canSpawn = false;
        }

        ShipLandTakeOff shipLandTakeOffScript = FindObjectOfType<ShipLandTakeOff>();

            // Handle spawning of digger
            if (canSpawn && Input.GetKey(KeyCode.Space) && Time.time >= lastSpawnTime + spawnDelay && shipLandTakeOffScript.playerExitAndEnterShip == true && shipLandTakeOffScript.playerGameObjectReference.activeSelf == false)
            {

                if (diggerPrefab != null)
                {
                    SpawnDigger();
                }
                lastSpawnTime = Time.time;
            }
        

        // Check for the nearest planet
        CheckForClosestPlanet();

        // Move if active
        if (isMoving && target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void SpawnDigger()
    {
        GameObject digger = Instantiate(diggerPrefab, transform.position, Quaternion.identity);
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



