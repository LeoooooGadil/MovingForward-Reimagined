using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorDisplay : MonoBehaviour
{
    public Color defaultColor;
    public Color winColor;
    public Color loseColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<RawImage>().color;
    }

    public void SetWinColor()
    {
        GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, winColor, 0.3f);
    }

    public void SetLoseColor()
    {
        GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, loseColor, 0.3f);
    }

    public void SetDefaultColor()
    {
        GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, defaultColor, 0.3f);
    }
}
