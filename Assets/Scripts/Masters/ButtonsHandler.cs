using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ButtonsHandler : MonoBehaviour
{
    public event Action OnWipeRecord;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Field");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void WipeRecord()
    {
        if(OnWipeRecord != null)
            OnWipeRecord();
    }
}
