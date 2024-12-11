using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHarvest : MonoBehaviour
{
    public Texture2D planetTexture; // The original texture used by the planet
    public int pixelRadius = 5; // Radius of the transparent area
    public float detectionRadius = 0.5f; // Radius to detect "Dig" objects
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Cache the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Clone the texture to avoid modifying the shared asset
        Texture2D originalTexture = spriteRenderer.sprite.texture;
        planetTexture = new Texture2D(originalTexture.width, originalTexture.height, originalTexture.format, false);

        // Copy pixel data from the original texture to the new texture
        planetTexture.SetPixels(originalTexture.GetPixels());
        planetTexture.Apply();

        // Create a new sprite using the cloned texture and assign it to the SpriteRenderer
        spriteRenderer.sprite = Sprite.Create(
            planetTexture,
            new Rect(0, 0, planetTexture.width, planetTexture.height),
            new Vector2(0.5f, 0.5f), // Pivot at the center
            spriteRenderer.sprite.pixelsPerUnit
        );
    }

    void Update()
    {
        // Find all "Dig" objects within detectionRadius of the planet
        Collider2D[] digObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (var digObject in digObjects)
        {
            if (digObject.CompareTag("Dig"))
            {
                // Get the position of the "Dig" object in world coordinates
                Vector2 digPosition = digObject.transform.position;

                // Convert world point to local point relative to the sprite's transform
                Vector2 localPoint = transform.InverseTransformPoint(digPosition);

                // Convert local point to normalized texture coordinates
                Vector2 normalizedPoint = new Vector2(
                    (localPoint.x + (spriteRenderer.sprite.bounds.size.x / 2)) / spriteRenderer.sprite.bounds.size.x,
                    (localPoint.y + (spriteRenderer.sprite.bounds.size.y / 2)) / spriteRenderer.sprite.bounds.size.y);

                // Convert normalized coordinates to pixel coordinates in the texture
                int x = Mathf.RoundToInt(normalizedPoint.x * planetTexture.width);
                int y = Mathf.RoundToInt(normalizedPoint.y * planetTexture.height);

                // Check if the pixel at this position is white
                if (IsWhitePixel(x, y))
                {
                    // Make the white pixel transparent
                    AddTransparentPixel(new Vector2(x, y));
                    return; // Exit after processing one pixel per frame
                }
            }
        }
    }

    bool IsWhitePixel(int x, int y)
    {
        if (x >= 0 && x < planetTexture.width && y >= 0 && y < planetTexture.height)
        {
            Color color = planetTexture.GetPixel(x, y);
            // Check if the pixel is close to white (adjust threshold as needed)
            return color.r > 0.9f && color.g > 0.9f && color.b > 0.9f && color.a > 0.9f;
        }
        return false;
    }

    void AddTransparentPixel(Vector2 pixelCoords)
    {
        int x = (int)pixelCoords.x;
        int y = (int)pixelCoords.y;

        // Modify the texture to add transparency at the calculated pixel coordinates
        for (int i = -pixelRadius; i <= pixelRadius; i++)
        {
            for (int j = -pixelRadius; j <= pixelRadius; j++)
            {
                int nx = x + i;
                int ny = y + j;
                if (nx >= 0 && nx < planetTexture.width && ny >= 0 && ny < planetTexture.height)
                {
                    float distance = Mathf.Sqrt(i * i + j * j);
                    if (distance <= pixelRadius)
                    {
                        Color color = planetTexture.GetPixel(nx, ny);
                        color.a = 0; // Set the alpha to 0 for transparency
                        planetTexture.SetPixel(nx, ny, color);
                    }
                }
            }
        }

        // Apply the changes to the texture
        planetTexture.Apply();

        // Remove the existing polygon collider
        Destroy(GetComponent<PolygonCollider2D>());

        // Add a new polygon collider to match the updated shape
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
