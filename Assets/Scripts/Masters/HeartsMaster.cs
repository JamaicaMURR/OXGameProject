using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Bycicles.Ranges;

public class HeartsMaster : MonoBehaviour
{
    int _units;

    public bool isNoUnits;

    public int Units
    {
        get { return _units; }
        set
        {
            if(value <= 0)
            {
                if(OnZeroUnits != null)
                    OnZeroUnits();

                isNoUnits = true;
            }
            else if(value < _units)
            {
                if(OnUnitLost != null)
                    OnUnitLost();
            }

            _units = value.ExNotBelow(0, "Hearts value").NotAbove(unitImages.Length);

            HighlightUnits(_units);
        }
    }

    public int MaximalUnits { get { return unitImages.Length; } }

    public Image[] unitImages;

    public event Action OnZeroUnits;
    public event Action OnUnitLost;

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
