using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
    public GameObject ghostPrefab;

    protected GameObject ghost;
    protected Transform ghostTransform;

    void Start()
    {
        Spawn();
    }

    public void Pull()
    {
        ghostTransform.position = new Vector3(transform.position.x, transform.position.y, ghostTransform.position.z);
    }

    public void Delete()
    {
        if(ghost != null)
            Destroy(ghost);
    }

    public void Spawn()
    {
        ghost = Instantiate(ghostPrefab);
        ghostTransform = ghost.GetComponent<Transform>();
        Pull();
    }
}
