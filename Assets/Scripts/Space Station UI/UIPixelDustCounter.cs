using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;


public class UIPixelDustCounter : MonoBehaviour
{

    public TextMeshProUGUI uiText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject counterGameObject = GameObject.Find("Game Logic");
        PixelDustCounter pixelDustValue = counterGameObject.GetComponent<PixelDustCounter>();

        if(pixelDustValue.pixelDustCounter < 0)
        {
            uiText.text = Convert.ToString(0);

        } else if (pixelDustValue.pixelDustCounter >= 0)
        {
            uiText.text = Convert.ToString(pixelDustValue.pixelDustCounter);
        }
    }
}
