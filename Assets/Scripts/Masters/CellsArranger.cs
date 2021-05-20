using UnityEngine;
using System.Collections;

public class CellsArranger : MonoBehaviour
{
    static System.Random random = new System.Random();

    public NetMaster netMaster;
    public GameObject cellPrefab;

    void Start()
    {
        Arrange();
    }

    public void Arrange()
    {
        for(int x = 0; x < netMaster.netWidth; x++)
            for(int y = 0; y < netMaster.netHeight; y++)
            {
                GameObject newbie = Instantiate(cellPrefab);

                newbie.GetComponent<NetMember>().Position = new NetPosition(x, y);

                if(netMaster.GetDefCellState(new NetPosition(x, y)) == CellState.Empty)
                    newbie.GetComponent<CellTile>().SetRandomSpriteFromSetNumber(1);
                else
                    newbie.GetComponent<CellTile>().SetRandomSpriteFromSetNumber(2);

                newbie.GetComponent<Transform>().Rotate(0, 0, 90 * random.Next(0, 4));
            }
    }
}
