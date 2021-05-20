using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{
    [HideInInspector]
    public GameObject ghost;

    public GameObject ghostPrefab;

    public float transparency = 0.125f;

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

        Pull();
    }

    public void CloneSprite()
    {
        SpriteRenderer ghostSpriteRenderer = ghost.GetComponent<SpriteRenderer>();
        SpriteRenderer originalSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        ghostSpriteRenderer.sprite = originalSpriteRenderer.sprite;
        ghostSpriteRenderer.flipX = originalSpriteRenderer.flipX;
        ghostSpriteRenderer.flipY = originalSpriteRenderer.flipY;

        ghostSpriteRenderer.color = new Color(ghostSpriteRenderer.color.r, ghostSpriteRenderer.color.g, ghostSpriteRenderer.color.b, transparency);
    }

    public void SetSprite(Sprite sprite)
    {
        SpriteRenderer ghostSpriteRenderer = ghost.GetComponent<SpriteRenderer>();

        ghostSpriteRenderer.sprite = sprite;
        ghostSpriteRenderer.color = new Color(ghostSpriteRenderer.color.r, ghostSpriteRenderer.color.g, ghostSpriteRenderer.color.b, transparency);
    }
}
