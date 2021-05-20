using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    int _currentFrame;

    float _frameDelay;

    SpriteRenderer spriteRenderer;

    public Sprite[] frames;
    public float animationTime = 1;

    public event Action OnAnimationEnd;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        _frameDelay = animationTime / frames.Length;
    }

    public void StartAnimation()
    {
        StartCoroutine("Animation");
    }

    IEnumerator Animation()
    {
        foreach(Sprite sprite in frames)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(_frameDelay);
        }

        if(OnAnimationEnd != null)
            OnAnimationEnd();

        yield break;
    }
}
