using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TintMenuInfoTextScript : MonoBehaviour
{
    public CentralPort central;
    public Text textField;

    public Color regularColor;
    public Color warningColor;

    public string noRecordMessage = "No new record";
    public string newRecordMessage = "New record!";
    public string underRecordMessage = " under record";
    public string overRecordMessage = " over record";

    private void OnEnable()
    {
        ShowMessage();
    }

    void ShowMessage()
    {
        int record = PlayerPrefs.GetInt("record");

        if(central.heartsMaster.isNoUnits)
        {
            if(central.pointsMaster.points < record)
            {
                textField.color = regularColor;
                textField.text = noRecordMessage;
            }
            else
            {
                textField.color = warningColor;
                textField.text = newRecordMessage;
            }
        }
        else
        {
            if(central.pointsMaster.points < record)
            {
                textField.color = regularColor;
                textField.text = record - central.pointsMaster.points + underRecordMessage;
            }
            else
            {
                textField.color = warningColor;
                textField.text = central.pointsMaster.points - record + overRecordMessage;
            }
        }
    }
}
