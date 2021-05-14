using UnityEngine;
using System.Collections;

public class Orangator : MonoBehaviour
{
    public Sprite sprite;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SwitchSprite()
    {
        Sprite oldSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = sprite;
        sprite = oldSprite;
    }
}
