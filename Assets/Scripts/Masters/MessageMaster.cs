using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageMaster : MonoBehaviour
{
    public CentralPort central;
    public Text textField;

    string currentMessage;

    void Awake()
    {
        central.inputHandler.OnPause += PauseMessage;
        central.inputHandler.OnUnPause += SetClean;
        central.inputHandler.OnPauserUsing += PauserUsedMessage;
        central.inputHandler.OnLock += LockUsedMessage;
    }



    void Update()
    {

    }

    void SetClean()
    {
        textField.text = "";
    }

    void PauseMessage()
    {
        string message = "Pause";

        textField.text = message;
    }

    void PauserUsedMessage()
    {
        textField.text = "Pauser used";
    }

    void LockUsedMessage()
    {
        textField.text = "Game Over";
    }
}
