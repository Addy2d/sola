using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelDustCounter : MonoBehaviour
{
    public int pixelDustCounter = 0;

    public void pixelDustIncrease()
    {
        pixelDustCounter = pixelDustCounter + 1;
    }

    public void pixelDustDecrease()
    {
        pixelDustCounter = pixelDustCounter - 1;
    }
}
