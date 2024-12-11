using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShipAudoDigger : MonoBehaviour
{
    public GameObject autoDiggerHousePrefab;       // Prefab for the digger
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
            canSpawn = true;

        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            canSpawn = false;

        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            canSpawn = false;
        }

        ShipLandTakeOff shipLandTakeOffScript = FindObjectOfType<ShipLandTakeOff>();
        // Handle spawning of digger

            if (canSpawn && Input.GetKey(KeyCode.Space) && Time.time >= lastSpawnTime + spawnDelay && shipLandTakeOffScript.playerExitAndEnterShip == true && shipLandTakeOffScript.playerGameObjectReference.activeSelf == false)
            {
                SpawnDiggerHouse();

                lastSpawnTime = Time.time;
            }
    }

    private void SpawnDiggerHouse()
    {
        GameObject diggerHouse = Instantiate(autoDiggerHousePrefab, transform.position, Quaternion.identity);
    }

    public void shipAutoDiggerRateIncrease()
    {
        spawnDelay = spawnDelay - 0.1f;
    }
}