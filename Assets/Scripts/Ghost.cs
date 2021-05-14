using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
    public GameObject ghostPrefab;

    GameObject _ghost;

    void Start()
    {
        Spawn();
    }

    public void Pull()
    {
        _ghost.GetComponent<Transform>().position = transform.position;
    }

    public void Delete()
    {
        Destroy(_ghost);
    }

    public void Spawn()
    {
        _ghost = Instantiate(ghostPrefab);
        Pull();
    }
}
