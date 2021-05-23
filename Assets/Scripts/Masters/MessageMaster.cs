using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MessageMaster : MonoBehaviour
{
    float _timer;

    Queue<string> messagesQueue;

    public CentralPort central;
    public Text textField;

    public Font regularFont;
    public Font boldFont;

    public Color warning;
    public Color attention;
    public Color regular;

    public float messageTime = 1;

    void Awake()
    {
        central.inputHandler.OnPause += delegate () { ShowMessage("Pause", regular); };
        central.inputHandler.OnUnPause += SetClean;
        central.inputHandler.OnPauserUsing += delegate () { ShowMessage("Light speed!", attention); };

        central.heartsMaster.OnUnitLost += delegate () { ShowMessage("Life lost!", warning); };
        central.heartsMaster.OnZeroUnits += delegate () { ShowMessage("Game Over", warning); };

        central.mergeMaster.AtMerged += delegate (int i)
        {
            if(i > 1)
            {
                string text = i + " merged!";

                if(i > 8)
                    ShowMessage("Unbelievable! " + text, attention);
                else if(i > 6)
                    ShowMessage("Aweesome!" + text, attention);
                else if(i > 4)
                    ShowMessage("Nice!" + text, attention);
                else if(i > 2)
                    ShowMessage(text, regular);
            }
        };
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= messageTime)
            SetClean();
    }

    void ShowMessage(string message, Color color)
    {
        ShowMessage(message, color, false);
    }

    void ShowMessage(string message, Color color, bool bold)
    {
        if(bold)
            textField.font = boldFont;
        else
            textField.font = regularFont;

        textField.color = color;
        textField.text = message;
        _timer = 0;
    }

    void SetClean()
    {
        textField.text = "";
        _timer = 0;
    }
}
