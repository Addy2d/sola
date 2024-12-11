using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade2 : MonoBehaviour
{
    public int updagradeCost = 0;           // Pixel Dust cost of upgrade
    public int increaseCostValue = 0;
    public bool canClick = false;

    public Material fadeMaterial; // Assign the material with the FadeShader
    public float fadeSpeed = 3.0f;

    private float fadeAmount;     // Current fade value
    private bool isFading = false;  // Tracks if fading is ongoing
    private bool fadeIn = true;    // Determines the direction of fading
    private bool fadeFinnished = false;

    public float increaseAutoDiggerFireRate; // Increase Ship Magnet size by this amount


    void Start()
    {
        fadeMaterial.SetFloat("_Fade", 0f);
    }

    void Update()
    {

        GameObject counterGameObject = GameObject.Find("Game Logic");
        PixelDustCounter pixelDustValue = counterGameObject.GetComponent<PixelDustCounter>();

        if (pixelDustValue.pixelDustCounter < updagradeCost)
        {
            canClick = false;

        }
        else if (pixelDustValue.pixelDustCounter >= updagradeCost && fadeFinnished)
        {
            canClick = true;
        }

        // If fading is active, update the fade amount
        if (isFading)
        {
            if (fadeIn && fadeAmount < 1.0f)
            {
                fadeAmount += fadeSpeed * Time.deltaTime;
            }
            else if (!fadeIn && fadeAmount > 0.0f)
            {
                fadeAmount -= fadeSpeed * Time.deltaTime;
            }
            else
            {
                // Stop fading when fadeAmount reaches its target
                isFading = false;
            }

            // Clamp the fadeAmount between 0 and 1
            fadeAmount = Mathf.Clamp01(fadeAmount);

            // Update the material's _Fade property
            if (fadeMaterial != null)
            {
                fadeMaterial.SetFloat("_Fade", fadeAmount);
            }
        }
    }

    public void TriggerFadeIn()
    {
        fadeIn = true;
        isFading = true;
        fadeFinnished = true;
    }

    public void TriggerFadeOut()
    {
        fadeIn = false;
        isFading = true;
        fadeFinnished = false;
    }

    public void OnMouseDown()
    {
        if (canClick)
        {
            PixelDustCounter counterScript = FindObjectOfType<PixelDustCounter>();

            for (int i = 0; i <= updagradeCost; i++)
            {
                counterScript.pixelDustDecrease();
            }

            updagradeCost = updagradeCost + increaseCostValue;

            WeaponShipAudoDigger autoDiggerSpawnScript = FindObjectOfType<WeaponShipAudoDigger>(); // Increase Ship Magnet Capture Size

            for (int i = 0; i <= increaseAutoDiggerFireRate; i++)
            {
                autoDiggerSpawnScript.shipAutoDiggerRateIncrease();
            }
        }
    }
}
