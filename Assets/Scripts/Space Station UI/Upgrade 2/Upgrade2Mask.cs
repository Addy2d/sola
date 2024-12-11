using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade2Mask : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component on this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Upgrade2 upgrade2Script = FindObjectOfType<Upgrade2>();

        if (upgrade2Script.canClick)
        {
            spriteRenderer.sortingOrder = 0;

        }
        else
        {
            spriteRenderer.sortingOrder = 3;
        }

    }
}
