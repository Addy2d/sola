using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrade1Text : MonoBehaviour
{
    public TextMeshPro uiText; // For world-space text

    void Update()
    {
        Upgrade1 upgrade1Script = FindObjectOfType<Upgrade1>();

        uiText.text = upgrade1Script.updagradeCost.ToString();
    }
}
