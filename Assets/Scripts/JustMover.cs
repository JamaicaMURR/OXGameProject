using UnityEngine;
using System.Collections;
using System;

public class JustMover : MonoBehaviour
{
    public event Action OnMovingFinish;

    public void Move(Vector3 target, float length)
    {
        Vector3 dash = target - transform.position;

        float maximalDashLength = length;

        if(dash.magnitude > maximalDashLength)
        {
            dash.Normalize();
            dash *= maximalDashLength;
            transform.Translate(dash);
        }
        else
        {
            transform.Translate(dash);

            if(OnMovingFinish != null)
                OnMovingFinish.Invoke();
        }

    }
}
