using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Module_Aim : MonoBehaviour
{
    public Transform multiToolPositionReference; // The point where we will shoot the projectile from
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float projectileSpeed = 10f; // Speed of the projectile

    public int maxPrefabsToFire = 3; // Change this to the desired number of prefabs to fire before reloading
    public float reloadTime = 0.5f; // Change this to the desired reload time in seconds
    private int prefabsFired = 0;
    public bool isReloading = false;

    public float clickFireRate = 0.5f; // Rate of fire on click
    public float continuousFireRate = 0.1f; // Rate of continuous fire when holding down the mouse button
    private float nextFireTime;

    private void Update()
    {

        // Check if reloading
        if (isReloading)
        {
            return;
        }


        // Check for mouse input to fire prefabs
        if (Input.GetMouseButtonDown(0))
        {

            // Projectile_Direction projectileDirectionScript = FindObjectOfType<Projectile_Direction>();
            // if(!projectileDirectionScript.multiToolInsidePlanet)
            // {
                FirePrefab();
                nextFireTime = Time.time + 1f / clickFireRate;
            // }
        }

        // Check for continuous shooting while holding the mouse button
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
           //  Projectile_Direction projectileDirectionScript = FindObjectOfType<Projectile_Direction>();
           //  if (!projectileDirectionScript.multiToolInsidePlanet)
           // {
                FirePrefab();
                nextFireTime = Time.time + 1f / continuousFireRate;
           // }
        }

        // Check for manual reload input
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void FirePrefab()
    {
        Projectile_Direction projectileDirectionScript = FindObjectOfType<Projectile_Direction>();
        // Check if the maximum number of prefabs has been fired
        if (prefabsFired < maxPrefabsToFire)
        {
            // Instantiate projectile prefab at the position of multitool game object
            GameObject projectileBullet = Instantiate(projectilePrefab, multiToolPositionReference.position, Quaternion.identity);

            // Apply a velocity to the projectile
            Rigidbody2D rb = projectileBullet.GetComponent<Rigidbody2D>();
            rb.velocity = projectileDirectionScript.direction.normalized * projectileSpeed; // Adjust the speed as per your requirement


            // Increment the count of prefabs fired
            prefabsFired++;

            // Check if it's time to reload
            if (prefabsFired == maxPrefabsToFire)
            {
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload()
    {
        // Set reloading flag to true
        isReloading = true;

        // Wait for the specified reload time
        yield return new WaitForSeconds(reloadTime);

        // Reset variables for the next round of firing
        prefabsFired = 0;
        isReloading = false;
    }

}
