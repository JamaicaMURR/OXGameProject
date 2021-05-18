using UnityEngine;
using System.Collections;
using Bycicles.Ranges;

public class Colorator : MonoBehaviour
{
    SpriteRenderer _rendrer;

    public Color initialColor;
    public Color targetColor;

    void Awake()
    {
        _rendrer = GetComponent<SpriteRenderer>();
        initialColor = _rendrer.color;
    }

    public void Paint(float exponent)
    {
        exponent.ExNotBelow(0, "Color exponent").ExNotAbove(1, "Color exponent");

        float r = (targetColor.r - initialColor.r) * exponent + initialColor.r;
        float g = (targetColor.g - initialColor.g) * exponent + initialColor.g;
        float b = (targetColor.b - initialColor.b) * exponent + initialColor.b;

        _rendrer.color = new Color(r, g, b);
    }

    public void Paint()
    {
        Paint(1);
    }

    public void Reset()
    {
        _rendrer.color = initialColor;
    }
}
