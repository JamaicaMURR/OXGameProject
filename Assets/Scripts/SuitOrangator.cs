using UnityEngine;
using System.Collections;

public class SuitOrangator : MonoBehaviour
{
    public Sprite[] orangeSuits;

    SpriteRenderer _spriteRenderer;
    SuitChanger _suitChanger;

    void Awake()
    {
        _suitChanger = GetComponent<SuitChanger>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_suitChanger == null)
            throw new System.Exception("Can't find SuitChanger component");

        if(_spriteRenderer == null)
            throw new System.Exception("Can't find SpriteRenderer component");

        if(orangeSuits.Length != _suitChanger.suits.Length)
            throw new System.Exception("Suits number mismatch");
    }

    public void OrangateSuit()
    {
        _spriteRenderer.sprite = orangeSuits[_suitChanger.lastSuitNumber];
        _spriteRenderer.flipX = _suitChanger.xWasFliped;
        _spriteRenderer.flipY = _suitChanger.yWasFliped;
    }
}
