using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationRotate : MonoBehaviour
{
    // Set the speed of rotation (degrees per second)
    public float rotationSpeed = 90f;

    public float dragSpeed = 5f;  // Speed at which the object will move towards the center.
    public GameObject targetGameObject;  // The target object to be dragged
    private bool isFrozen = false;  // Flag to track if the object is frozen
    private Rigidbody2D targetRigidbody2D;  // Reference to the target object's Rigidbody2D

    private void Start()
    {
        // Ensure that the target game object is assigned
        if (targetGameObject == null)
        {
            Debug.LogError("Target GameObject is not assigned!");
        }

        // Get the Rigidbody2D component, if available
        targetRigidbody2D = targetGameObject.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        // Rotate the object around the Z-axis (2D rotation) at a set speed
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Check for the "Exit" key press (default: Escape key) to unfreeze the object
        if (isFrozen && Input.GetKeyDown(KeyCode.Escape))
        {
            UnfreezePosition();

            // Find all GameObjects with the UpgradeTextFade script
            UpgradeTextFade[] upgradeTextFadeScripts = FindObjectsOfType<UpgradeTextFade>();

            // Loop through each script instance and trigger the function
            foreach (UpgradeTextFade upgradeTextFadeScript in upgradeTextFadeScripts)
            {
                if (upgradeTextFadeScript != null) // Ensure the script exists
                {
                    upgradeTextFadeScript.TriggerFadeOut();
                }
            }

            Upgrade1 upgrade1Script = FindObjectOfType<Upgrade1>();
            upgrade1Script.TriggerFadeOut();

            Upgrade2 upgrade2Script = FindObjectOfType<Upgrade2>();
            upgrade2Script.TriggerFadeOut();

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Start dragging the object to the center when it enters the trigger
        if (other.gameObject == targetGameObject)
        {
            StartCoroutine(DragToCenterCoroutine(targetGameObject));

            // Find all GameObjects with the UpgradeTextFade script
            UpgradeTextFade[] upgradeTextFadeScripts = FindObjectsOfType<UpgradeTextFade>();

            // Loop through each script instance and trigger the function
            foreach (UpgradeTextFade upgradeTextFadeScript in upgradeTextFadeScripts)
            {
                if (upgradeTextFadeScript != null) // Ensure the script exists
                {
                    upgradeTextFadeScript.TriggerFadeIn();
                }
            }

            Upgrade1 upgrade1Script = FindObjectOfType<Upgrade1>();
            upgrade1Script.TriggerFadeIn();

            Upgrade2 upgrade2Script = FindObjectOfType<Upgrade2>();
            upgrade2Script.TriggerFadeIn();

        }
    }

    private IEnumerator DragToCenterCoroutine(GameObject target)
    {
        Vector2 targetPosition = transform.position;  // The center position of the object with the trigger

        // Move the object towards the center until it reaches it
        while ((Vector2)target.transform.position != targetPosition)
        {
            target.transform.position = Vector2.MoveTowards(target.transform.position, targetPosition, dragSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure it reaches the exact center
        target.transform.position = targetPosition;

        // Freeze the position when it reaches the center
        FreezePosition();
    }

    private void FreezePosition()
    {
        isFrozen = true;

        if (targetRigidbody2D != null)
        {
            // Freeze the Rigidbody2D's position by applying Rigidbody constraints
            targetRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void UnfreezePosition()
    {
        isFrozen = false;

        if (targetRigidbody2D != null)
        {
            // Remove the position freeze, allowing the Rigidbody to move freely
            targetRigidbody2D.constraints = RigidbodyConstraints2D.None;
        }

    }


}
