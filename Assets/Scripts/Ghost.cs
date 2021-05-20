using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
    public GameObject ghostPrefab;
    public GameObject ghost;

    protected Transform ghostTransform;

    public void Pull()
    {
        ghostTransform.position = new Vector3(transform.position.x, transform.position.y, ghostTransform.position.z);
        ghostTransform.rotation = transform.rotation;
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

        SpriteRenderer ghostSpriteRenderer = ghost.GetComponent<SpriteRenderer>();
        SpriteRenderer originalSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        ghostSpriteRenderer.sprite = originalSpriteRenderer.sprite;
        ghostSpriteRenderer.flipX = originalSpriteRenderer.flipX;
        ghostSpriteRenderer.flipY = originalSpriteRenderer.flipY;

        Pull();
    }
}
