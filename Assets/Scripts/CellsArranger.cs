﻿using UnityEngine;
using System.Collections;

public class CellsArranger : MonoBehaviour
{
    public OXNetMaster netMaster;
    public GameObject cellPrefab;

    // Use this for initialization
    void Start()
    {
        Arrange();
    }

    public void Arrange()
    {
        for(int x=0; x<netMaster.netWidth; x++)
            for(int y=0; y<netMaster.netHeight; y++)
            {
                GameObject newbie = Instantiate(cellPrefab);

                newbie.GetComponent<OXNetMember>().NetPosition = new OXNetPosition(x, y);

                if(netMaster.GetDefCellState(new OXNetPosition(x, y)) == CellState.Empty)
                    newbie.GetComponent<CellTile>().SetRandomSpriteFromSetNumber(1);
                else
                    newbie.GetComponent<CellTile>().SetRandomSpriteFromSetNumber(2);
            }
    }
}
