using UnityEngine;
using System;

public class SpriteSwaper : MonoBehaviour
{
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        if(spriteRenderer == null)
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if(spriteRenderer == null)
            throw new Exception("Can't find SpriteRenderer component");
    }

    public void SwapSprite()
    {
        Sprite oldSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = sprite;
        sprite = oldSprite;
    }
}
