using UnityEngine;
using System.Collections;

public class CellsArranger : MonoBehaviour
{
    static System.Random random = new System.Random();

    public OXNetMaster netMaster;
    public GameObject cellPrefab;

    // Use this for initialization
    void Start()
    {
        Arrange();
    }

    public void Arrange()
    {
        for(int x = 0; x < netMaster.netWidth; x++)
            for(int y = 0; y < netMaster.netHeight; y++)
            {
                if(!((x == 0 || x == netMaster.netWidth - 1) && (y == 0 || y == netMaster.netHeight - 1)))
                {
                    GameObject newbie = Instantiate(cellPrefab);

                    newbie.GetComponent<OXNetMember>().NetPosition = new OXNetPosition(x, y);

                    if(netMaster.GetDefCellState(new OXNetPosition(x, y)) == CellState.Empty)
                        newbie.GetComponent<CellTile>().SetRandomSpriteFromSetNumber(1);
                    else
                        newbie.GetComponent<CellTile>().SetRandomSpriteFromSetNumber(2);

                    newbie.GetComponent<Transform>().Rotate(0, 0, 90 * random.Next(0, 4));
                }
            }
    }
}
