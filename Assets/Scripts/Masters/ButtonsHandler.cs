using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonsHandler : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
            LoadField();
    }

    public void LoadField()
    {
        SceneManager.LoadScene("Field");
    }
}
