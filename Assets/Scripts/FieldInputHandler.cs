using UnityEngine;
using System.Collections;

public class FieldInputHandler : MonoBehaviour
{
    bool isPaused = false;

    public XBehavior xBehavior;

    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;

            if(isPaused)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        if(Input.GetButtonDown("Up"))
            xBehavior.TryToMove(Direction.Up);

        if(Input.GetButtonDown("Down"))
            xBehavior.TryToMove(Direction.Down);

        if(Input.GetButtonDown("Left"))
            xBehavior.TryToMove(Direction.Left);

        if(Input.GetButtonDown("Right"))
            xBehavior.TryToMove(Direction.Right);
    }
}
