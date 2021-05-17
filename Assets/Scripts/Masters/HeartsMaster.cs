using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Bycicles.Ranges;

public class HeartsMaster : MonoBehaviour
{
    int _hearts;

    public int Hearts
    {
        get { return _hearts; }
        set
        {
            _hearts = value.ExNotBelow(0, "Hearts value").NotAbove(heartImages.Length);

            if(_hearts == 0)
                if(OnZeroHearts != null)
                    OnZeroHearts();

            HighlightHearts(_hearts);
        }
    }

    public Image[] heartImages;

    public event Action OnZeroHearts;

    void Start()
    {
        Hearts = heartImages.Length;
    }

    void HighlightHearts(int number)
    {
        for(int i = 0; i < heartImages.Length; i++)
        {
            if(i < number)
                heartImages[i].enabled = true;
            else
                heartImages[i].enabled = false;
        }
    }
}
