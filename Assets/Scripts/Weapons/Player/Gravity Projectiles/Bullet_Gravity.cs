using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Gravity : MonoBehaviour
{
    private Transform planetReference;
    public float gravityStrength = 1f;
    private Rigidbody2D rb;
    public float destroyDelay = 3f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerMovementPlanet playerPlanetScript = FindObjectOfType<PlayerMovementPlanet>();
        if (playerPlanetScript != null && playerPlanetScript.closestPlanet != null)
        {
            planetReference = playerPlanetScript.closestPlanet;
            Vector2 gravityDirection = (planetReference.position - transform.position).normalized;
            Vector2 gravityForce = gravityDirection * gravityStrength;
            rb.AddForce(gravityForce);
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        if (gameObject.name == "Bullet(Clone)")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding GameObject has a specific tag
        if (collision.gameObject.tag == "PlanetPiece")
        {
             Destroy(gameObject);
        }
    }
}
