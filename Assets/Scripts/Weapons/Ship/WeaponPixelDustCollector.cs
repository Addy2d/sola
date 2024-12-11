using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPixelDustCollector : MonoBehaviour
{
    public GameObject pixelDustCollector;       // Prefab for the digger
    public float spawnDelay = 1f;         // Delay in seconds before next spawn
    private float lastSpawnTime = 0f;     // Time of last spawn

    public bool canSpawn = false;

    void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            canSpawn = false;

        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            canSpawn = false;

        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            canSpawn = true;

        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            canSpawn = false;
        }

        ShipLandTakeOff shipLandTakeOffScript = FindObjectOfType<ShipLandTakeOff>();
        // Handle spawning of digger

        if (canSpawn && Input.GetKey(KeyCode.Space) && Time.time >= lastSpawnTime + spawnDelay && shipLandTakeOffScript.playerExitAndEnterShip == true && shipLandTakeOffScript.playerGameObjectReference.activeSelf == false)
        {
            SpawnPixelDustCollector();

            lastSpawnTime = Time.time;
        }
    }

    private void SpawnPixelDustCollector()
    {
        GameObject pixelDustCollectorSprite = Instantiate(pixelDustCollector, transform.position, Quaternion.identity);
    }

    public void PixelDustCollectorCooldownDecrease()
    {
        spawnDelay = spawnDelay - 0.1f;
    }
}
