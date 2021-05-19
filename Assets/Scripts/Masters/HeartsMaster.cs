using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Bycicles.Ranges;

public class HeartsMaster : MonoBehaviour
{
    int _units;

    public int Units
    {
        get { return _units; }
        set
        {
            _units = value.ExNotBelow(0, "Hearts value").NotAbove(unitImages.Length);

            if(_units == 0)
                if(OnZeroUnits != null)
                    OnZeroUnits();

            HighlightUnits(_units);
        }
    }

    public int MaximalUnits { get { return unitImages.Length; } }

    public Image[] unitImages;

    public event Action OnZeroUnits;

    //============================================================================================================================================================================
    void Start()
    {
        Units = unitImages.Length;
    }

    //============================================================================================================================================================================
    void HighlightUnits(int number)
    {
        for(int i = 0; i < unitImages.Length; i++)
        {
            if(i < number)
                unitImages[i].enabled = true;
            else
                unitImages[i].enabled = false;
        }
    }
}
