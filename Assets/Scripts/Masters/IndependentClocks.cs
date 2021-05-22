using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentClocks : MonoBehaviour
{
    long _oldTicks;
    long _deltaTicks;

    public float DeltaTime { get { return (float)_deltaTicks / 10000000; } }

    private void Awake()
    {
        _oldTicks = DateTime.Now.Ticks;
    }

    void Update()
    {
        _deltaTicks = DateTime.Now.Ticks - _oldTicks;
        _oldTicks = DateTime.Now.Ticks;
    }
}
