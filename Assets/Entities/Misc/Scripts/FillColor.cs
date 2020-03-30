using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillColor : MonoBehaviour
{
    public Color GetColor()
    {
        return GetComponent<Image>().color;
    }

    public void SetColor(Color32 newColor)
    {
        GetComponent<Image>().color = newColor;
    }
}
