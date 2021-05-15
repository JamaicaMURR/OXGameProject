using UnityEngine;
using System.Collections;

public class SuitChanger : MonoBehaviour
{
    static System.Random random = new System.Random();

    SpriteRenderer _spriteRenderer;

    public Sprite[] suits;

    public bool changeAtStart = true;
    public bool randomFlipWhenChange = true;

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
        if(changeAtStart)
            ChangeSuit();
    }

    public void ChangeSuit(int number)
    {
        _spriteRenderer.sprite = suits[number];

        if(randomFlipWhenChange)
        {
            _spriteRenderer.flipX = random.NextDouble() < 0.5;
            _spriteRenderer.flipY = random.NextDouble() < 0.5;
        }
    }

    public void ChangeSuit()
    {
        ChangeSuit(random.Next(0, suits.Length));
    }
}
