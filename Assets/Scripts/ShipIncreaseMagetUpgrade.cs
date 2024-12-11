using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIncreaseMagetUpgrade : MonoBehaviour
{
    public void IncreaseCircleCollider()
    {
        // Get the CircleCollider2D component attached to this GameObject
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius += 0.1f;
    }
}
