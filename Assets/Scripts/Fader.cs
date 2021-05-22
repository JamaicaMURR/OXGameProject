using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    float expiredTime;

    delegate float FloatProvider();

    FloatProvider GetDeltaTime;
    Action DoOnUpdate;

    public Image image;

    public float maxTransparency;
    public float fadeDuration;

    public bool useIndependentClocks;
    public IndependentClocks indieClocks;

    public event Action OnFadeInEnd;
    public event Action OnFadeOutEnd;

    //==================================================================================================================================================================
    private void Awake()
    {
        if(useIndependentClocks)
            GetDeltaTime = delegate () { return indieClocks.DeltaTime; };
        else
            GetDeltaTime = delegate () { return Time.deltaTime; };

        DoOnUpdate = Idle;
    }

    private void Update()
    {
        DoOnUpdate();
    }

    //==================================================================================================================================================================
    public bool IsReady() { return DoOnUpdate == Idle; }

    public void StartFadeIn()
    {
        if(IsReady())
            DoOnUpdate = FadeIn;
    }

    public void StartFadeOut()
    {
        if(IsReady())
            DoOnUpdate = FadeOut;
    }

    //==================================================================================================================================================================
    void FadeIn()
    {
        expiredTime += GetDeltaTime();
        image.color = new Color(image.color.r, image.color.g, image.color.b, expiredTime / fadeDuration * maxTransparency);

        if(CheckEndOfFading())
            if(OnFadeInEnd != null)
                OnFadeInEnd();
    }

    void FadeOut()
    {
        expiredTime += GetDeltaTime();
        image.color = new Color(image.color.r, image.color.g, image.color.b, (1 - expiredTime / fadeDuration));

        if(CheckEndOfFading())
            if(OnFadeOutEnd != null)
                OnFadeOutEnd();
    }

    bool CheckEndOfFading()
    {
        bool result = false;

        if(expiredTime >= fadeDuration)
        {
            expiredTime = 0;
            DoOnUpdate = Idle;

            result = true;
        }

        return result;
    }

    void Idle() { }
}
