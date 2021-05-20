using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    int _currentFrame;

    SpriteRenderer spriteRenderer;

    [HideInInspector]
    public float animationTime;

    public Sprite[] frames;
    public float frameDelay;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        animationTime = frames.Length * frameDelay;
    }

    public void Animate()
    {
        StartCoroutine("Animation");
    }

    IEnumerator Animation()
    {
        foreach(Sprite sprite in frames)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(frameDelay);
        }
    }
}
