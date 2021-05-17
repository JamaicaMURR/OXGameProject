using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
    public GameObject ghostPrefab;

    protected GameObject ghost;

    void Start()
    {
        Spawn();
    }

    public void Pull()
    {
        ghost.GetComponent<Transform>().position = transform.position;
    }

    public void Delete()
    {
        Destroy(ghost);
    }

    public void Spawn()
    {
        ghost = Instantiate(ghostPrefab);
        Pull();
    }
}
