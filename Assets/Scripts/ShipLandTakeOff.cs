using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLandTakeOff : MonoBehaviour
{
    public GameObject playerGameObjectReference; // Assign the other game object in the Inspector
    public bool shipHasTouchedDown;
    public bool playerExitAndEnterShip = true;
    private PolygonCollider2D polygonCollider;
    public ShipMove movementScriptOn; // Reference to the script you want to toggle
    public int groundLayer = 3; // The layer number for ground
    public Transform shipTransform;

    public float speedThreshold = 0.1f; // Set your speed threshold
    private Vector2 lastPosition;
    private float speed;



    // Start is called before the first frame update
    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();

        // Initialize lastPosition with the object's initial position
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // Calculate the distance moved since the last frame
        Vector2 currentPosition = transform.position;
        float distance = Vector2.Distance(currentPosition, lastPosition);

        // Calculate speed (distance divided by time)
        speed = distance / Time.deltaTime;

        // Update last position
        lastPosition = currentPosition;

        
        // Check if the E key is pressed and ship is on planet
        if (shipHasTouchedDown == true && Input.GetKeyDown(KeyCode.E) && speed < speedThreshold)
        {
            if (playerExitAndEnterShip == true)
            {
                if (playerGameObjectReference.activeSelf == false) // Player Exits Ship
                {
                    // Turn Ship Polygon Collider Trigger Off so it reacts to other objects when flying:
                    polygonCollider.isTrigger = true;
                    // Turn Ship Movement Script Off:
                    movementScriptOn.enabled = false;
                    // Turn Player Game Object On
                    playerGameObjectReference.SetActive(true);
                    // Teleport the other game object to the position of the current game object
                    playerGameObjectReference.transform.position = transform.position;
                    // Switch camera follow from ship to player
                }
            }
            else // Player Enters Ship
            {
                // Turn Ship Polygon Collider to Trigger so it does not react to Player:
                polygonCollider.isTrigger = false;
                // Turn Ship Movement Script On:
                movementScriptOn.enabled = true;
                // Turn Player Game Object Off
                playerGameObjectReference.SetActive(false);
                // Switch camera follow from player to ship
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.name == "PlayerPlanet")
        {
            // Player has collided with ship
            playerExitAndEnterShip = false;
        }

        // Check if the collided object's layer is the same as the ground layer
        if (other.gameObject.layer == groundLayer)
        {
                // Ship is on planet
                shipHasTouchedDown = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "PlayerPlanet")
        {
            // Player has collided with ship
            playerExitAndEnterShip = false;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collided object's layer is the same as the ground layer
        if (other.gameObject.layer == groundLayer)
        {
            shipHasTouchedDown = false;

        }

        if (other.gameObject.name != "PlayerPlanet")
        {
            // Player has collided with ship
            playerExitAndEnterShip = true;
        }
    }

}
