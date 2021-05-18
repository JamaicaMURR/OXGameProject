using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecordKeeper : MonoBehaviour
{
    public Text textField;
    public string headString = "Record: ";

    void Start()
    {
        int actualRecord = PlayerPrefs.GetInt("record");


        textField.text = headString + actualRecord.ToString();
    }
}
