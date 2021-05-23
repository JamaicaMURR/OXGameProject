using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSpawner : MonoBehaviour
{
    public GameObject popupPrefab;

    public void Pop(string message)
    {
        GameObject newbie = Instantiate(popupPrefab, transform);

        newbie.GetComponent<Transform>().position = transform.position;
        newbie.GetComponent<Text>().text = message;
        newbie.GetComponent<Pop>().BeginEvaporating();
    }
}
