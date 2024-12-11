using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetIncreaseSprite : MonoBehaviour
{
    public void IncreaseMagnetSprite()
    {
        // Get the current scale of the GameObject
        Vector3 currentScale = transform.localScale;

        // Increase the scale on the X and Y axes
        currentScale.x += 0.1f;
        currentScale.y += 0.1f;

        // Set the new scale back to the GameObject
        transform.localScale = currentScale;
    }
}
