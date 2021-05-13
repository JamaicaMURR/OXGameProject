using UnityEngine;
using System.Collections;
using System;

public class OSettings : MonoBehaviour
{
    [SerializeField]
    float _speed;

    public event Action OnSpeedChange;

    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            OnSpeedChange();
        }
    }
}
