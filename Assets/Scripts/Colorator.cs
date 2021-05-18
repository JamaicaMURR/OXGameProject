using UnityEngine;
using System.Collections;
using Bycicles.Ranges;

public class Colorator : MonoBehaviour
{
    SpriteRenderer _rendrer;
    Color _initialColor;

    public Color targetColor;

    void Awake()
    {
        _rendrer = GetComponent<SpriteRenderer>();
        _initialColor = _rendrer.color;
    }

    public void Paint(float exponent)
    {
        exponent.ExNotBelow(0, "Color exponent").ExNotAbove(1, "Color exponent");

        float r = (targetColor.r - _initialColor.r) * exponent + _initialColor.r;
        float g = (targetColor.g - _initialColor.g) * exponent + _initialColor.g;
        float b = (targetColor.b - _initialColor.b) * exponent + _initialColor.b;

        _rendrer.color = new Color(r, g, b);
    }

    public void Paint()
    {
        Paint(1);
    }

    public void Reset()
    {
        _rendrer.color = _initialColor;
    }
}
