using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpriteSplit : MonoBehaviour
{
    public Sprite originalSprite; // Original sprite to be split
    public int rows = 10; // Number of rows to split
    public int cols = 10; // Number of columns to split

    public Vector3 shiftAmount = Vector3.zero; // Amount to shift the split sprite pieces
    public int planetNumber;

    private void Start()
    {
        SplitSprite();
    }

    private void SplitSprite()
    {
        // Calculate the size of each sprite piece
        float pieceWidth = originalSprite.bounds.size.x / cols;
        float pieceHeight = originalSprite.bounds.size.y / rows;

        // Loop through rows and columns to split the sprite
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Calculate the position for the current sprite piece
                Vector3 position = new Vector3(
                    transform.position.x + col * pieceWidth,
                    transform.position.y - row * pieceHeight,
                    transform.position.z
                );

                // Apply the shift amount
                position += shiftAmount;

                // Create a new GameObject for the sprite piece
                GameObject spritePiece = new GameObject("SpritePiece " + planetNumber);
                spritePiece.transform.position = position;

                // Add a SpriteRenderer component to the sprite piece
                SpriteRenderer spriteRenderer = spritePiece.AddComponent<SpriteRenderer>();

                // Calculate the UV coordinates for the sprite piece
                Rect spriteRect = new Rect(
                    col * (1.0f / cols),
                    1.0f - (row + 1) * (1.0f / rows),
                    1.0f / cols,
                    1.0f / rows
                );

                // Create a new sprite for the sprite piece
                Sprite pieceSprite = Sprite.Create(
                    originalSprite.texture,
                    new Rect(originalSprite.rect.width * spriteRect.x, originalSprite.rect.height * spriteRect.y, originalSprite.rect.width * spriteRect.width, originalSprite.rect.height * spriteRect.height),
                    new Vector2(0.5f, 0.5f)
                );

                // Set the sprite for the SpriteRenderer
                spriteRenderer.sprite = pieceSprite;

                // Parent the sprite piece to this GameObject
                spritePiece.transform.parent = transform;

                // Create Box Collider
                PolygonCollider2D collider = spritePiece.AddComponent<PolygonCollider2D>();

                // Attach a script to the new GameObject
                DestoryPlanetPiece scriptComponent = spritePiece.AddComponent<DestoryPlanetPiece>();

                // Set the tag for the sprite piece
                spritePiece.tag = "PlanetPiece";

                spritePiece.layer = LayerMask.NameToLayer("Planet");

                // Check if the piece is fully transparent
                if (IsPieceFullyTransparent(pieceSprite))
                {
                    Destroy(spritePiece); // Remove the sprite piece if it's fully transparent
                    continue; // Skip to the next iteration of the loop
                }

                spritePiece.isStatic = true;
            }
        }

        // Disable the original sprite renderer
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private bool IsPieceFullyTransparent(Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        Color[] pixels = texture.GetPixels((int)sprite.textureRect.x, (int)sprite.textureRect.y, (int)sprite.textureRect.width, (int)sprite.textureRect.height);

        foreach (Color pixel in pixels)
        {
            if (pixel.a > 0)
            {
                return false;
            }
        }

        return true;
    }
}
