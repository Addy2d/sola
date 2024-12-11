using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPixelDustCollectorMagnet : MonoBehaviour
{
    public string targetTag = "PixelDust"; // Tag of objects to attract
    public float moveSpeed = 2f; // Speed of movement
    public float detectionRadius = 5f; // Radius within which objects are attracted
    public int pixelDustCollectedAmount = 0;

    void Update()
    {
        // Find all objects with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obj in taggedObjects)
        {
            float distance = Vector2.Distance(transform.position, obj.transform.position);

            // If the object is within the detection radius, move it towards this GameObject
            if (distance <= detectionRadius)
            {
                obj.transform.position = Vector2.MoveTowards(
                    obj.transform.position,
                    transform.position,
                    moveSpeed * Time.deltaTime
                );
            }
        }
    }

    public void IncreasePixelDustCollectedAmount()
    {
        pixelDustCollectedAmount = pixelDustCollectedAmount + 1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PixelDustCounter counterScript = FindObjectOfType<PixelDustCounter>();
            for (int i = 0; i <= pixelDustCollectedAmount; i++)
            {
                counterScript.pixelDustIncrease();

                if(pixelDustCollectedAmount > 0)
                {
                    pixelDustCollectedAmount = pixelDustCollectedAmount - 1;
                }
            }
        }
    }
}
