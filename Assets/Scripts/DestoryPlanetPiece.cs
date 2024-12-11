using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryPlanetPiece : MonoBehaviour
{

    // This method is called when another Collider2D enters the trigger zone.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other Collider2D belongs to the GameObject you want to detect.
        if (other.CompareTag("Dig"))
        {

            Destroy(this.gameObject);
        }
    }
}
