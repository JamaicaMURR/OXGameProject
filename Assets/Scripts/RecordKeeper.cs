using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecordKeeper : MonoBehaviour
{
    public ButtonsHandler buttonsHandler;
    public Text textField;
    public string headString = "Record: ";

    void Awake()
    {
        buttonsHandler.OnWipeRecord += Wipe;
    }

    void Start()
    {
        Display();
    }

    public void Display()
    {
        textField.text = headString + PlayerPrefs.GetInt("record").ToString();
    }

    public void Wipe()
    {
        PlayerPrefs.SetInt("record", 0);
        Display();
    }
}
