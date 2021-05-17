using UnityEngine;
using System.Collections;
using System;

public class FieldInputHandler : MonoBehaviour
{
    bool isPaused = false;
    bool isPauserUsed = false;

    Action Check;

    public CentralPort port;
    public XBehavior xBehavior;

    void Awake()
    {
        Check = Idle;
    }

    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;

            if(isPaused)
            {
                Time.timeScale = 0;
                isPauserUsed = false;
                Check = CheckPauser;
            }
            else
            {
                Time.timeScale = 1;
                Check = Idle;
            }
        }

        if(!isPaused || isPauserUsed || port.pausersMaster.Hearts > 0)
        {
            if(Input.GetButtonDown("Up"))
            {
                xBehavior.TryToMove(Direction.Up);
                Check();
            }

            if(Input.GetButtonDown("Down"))
            {
                xBehavior.TryToMove(Direction.Down);
                Check();
            }

            if(Input.GetButtonDown("Left"))
            {
                xBehavior.TryToMove(Direction.Left);
                Check();
            }

            if(Input.GetButtonDown("Right"))
            {
                xBehavior.TryToMove(Direction.Right);
                Check();
            }
        }
    }

    void CheckPauser()
    {
        if(isPaused && !isPauserUsed)
        {
            port.pausersMaster.Hearts--;
            isPauserUsed = true;
            Check = Idle;
        }
    }

    void Idle() { }
}
