using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade2Text : MonoBehaviour
{
    public TextMeshPro uiText; // For world-space text

    void Update()
    {
        Upgrade2 upgrade2Script = FindObjectOfType<Upgrade2>();

        uiText.text = upgrade2Script.updagradeCost.ToString();
    }
}
