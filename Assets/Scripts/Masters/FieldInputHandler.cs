using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class FieldInputHandler : MonoBehaviour
{
    bool isPaused = false;
    bool isPauserUsed = false;
    bool isLocked = false;

    Action Check;

    public CentralPort port;
    public XBehavior xBehavior;

    public event Action OnPause;
    public event Action OnUnPause;
    public event Action OnPauserUsing;
    public event Action OnLock;
    public event Action OnEscape;

    void Awake()
    {
        xBehavior.OnSuccefulMove += CheckOnXMoving;
        Check = Idle;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) //<--------------------------------------------------! test only
            Retry();

        if(Input.GetKeyDown(KeyCode.L)) //<--------------------------------------------------! test only
            LockGame();

        if(Input.GetButtonDown("EscapeToMenu"))
            EscapeToMenu();

        if(!isLocked)
        {
            if(Input.GetButtonDown("Pause"))
            {
                isPaused = !isPaused;

                if(isPaused)
                {
                    Time.timeScale = 0;
                    Check = CheckPauser;

                    if(OnPause != null)
                        OnPause();
                }
                else
                {
                    Time.timeScale = 1;
                    isPauserUsed = false;
                    Check = Idle;

                    if(OnUnPause != null)
                        OnUnPause();
                }
            }

            if(!isPaused || isPauserUsed || port.pausersMaster.Units > 0)
            {
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
    }

    //============================================================================================================================================================================
    public void LockGame()
    {
        Time.timeScale = 0;
        isLocked = true;

        if(OnLock != null)
            OnLock();
    }

    public void Retry()
    {
        Time.timeScale = 1; // If retry used when game paused or locked

        SceneManager.LoadScene("Field");
    }

    public void EscapeToMenu()
    {
        if(OnEscape != null)
            OnEscape();

        Time.timeScale = 1; // If esccape used when game is paused or locked

        SceneManager.LoadScene("Menu");
    }

    //============================================================================================================================================================================
    void CheckOnXMoving()
    {
        Check(); // If isPauserUsed || !isPaused is will be == Idle, otherwise: == CheckPauser 
    }
    
    void CheckPauser()
    {
        if(isPaused && !isPauserUsed)
        {
            port.pausersMaster.Units--;
            isPauserUsed = true;
            Check = Idle;

            if(OnPauserUsing != null)
                OnPauserUsing();
        }
    }

    void Idle() { }
}
