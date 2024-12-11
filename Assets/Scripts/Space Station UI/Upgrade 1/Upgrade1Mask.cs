using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade1Mask : MonoBehaviour
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
        Upgrade1 upgrade1Script = FindObjectOfType<Upgrade1>();

        if (upgrade1Script.canClick)
        {
            spriteRenderer.sortingOrder = 0;

        } else
        {
            spriteRenderer.sortingOrder = 3;
        }

    }
}
