using UnityEngine;
using System.Collections;

public class CellTile : MonoBehaviour
{
    static System.Random random = new System.Random();

    SpriteRenderer _spriteRenderer;

    public Sprite[] spriteSet1;
    public Sprite[] spriteSet2;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_spriteRenderer == null)
            throw new System.Exception("Can't find SpriteRenderer component");

        if(spriteSet1.Length == 0 || spriteSet2.Length == 0)
            throw new System.Exception("At least one of spriteSets is empty");
    }

    Sprite[] Choose(int setNumber)
    {
        Sprite[] chosen;

        switch(setNumber)
        {
            case 1:
                chosen = spriteSet1;
                break;
            case 2:
                chosen = spriteSet2;
                break;
            default:
                throw new System.Exception("Wrong spriteSet number");
        }

        return chosen;
    }

    public void SetRandomSpriteFromSetNumber(int setNumber)
    {
        Sprite[] chosen = Choose(setNumber);

        _spriteRenderer.sprite = chosen[random.Next(0, chosen.Length)];
    }
}
