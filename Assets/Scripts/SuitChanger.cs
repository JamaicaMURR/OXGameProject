using UnityEngine;
using System.Collections;
using Bycicles.Ranges;

public class SuitChanger : MonoBehaviour
{
    static System.Random random = new System.Random();

    SpriteRenderer _spriteRenderer;

    public Sprite[] suits;

    public bool randomChangeAtStart = true;
    public bool randomFlipWhenChange = true;

    [HideInInspector]
    public int lastSuitNumber = -1;

    [HideInInspector]
    public bool xWasFliped, yWasFliped; // Indicates of fliiping of last suit

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_spriteRenderer == null)
            throw new System.Exception("Can't find SpriteRenderer component");

        if(suits.Length == 0)
            throw new System.Exception("No suits");
    }

    void Start()
    {
        if(randomChangeAtStart)
            ChangeSuit();
    }

    public void ChangeSuit(int suitNumber)
    {
        suitNumber.ExNotBelow(0, "suitNumber").ExNotAbove(suits.Length - 1, "suitNumber");

        _spriteRenderer.sprite = suits[suitNumber];

        if(randomFlipWhenChange)
        {
            // Indicators changes at random first & then real flipping of suit according to indicators
            xWasFliped = random.NextDouble() < 0.5;
            yWasFliped = random.NextDouble() < 0.5;

            _spriteRenderer.flipX = xWasFliped;
            _spriteRenderer.flipY = yWasFliped;
        }

        lastSuitNumber = suitNumber;
    }

    public void ChangeSuit()
    {
        ChangeSuit(random.Next(0, suits.Length));
    }
}
