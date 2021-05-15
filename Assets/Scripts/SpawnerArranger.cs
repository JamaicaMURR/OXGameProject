using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerArranger : MonoBehaviour
{
    public OXNetMaster netMaster;
    public SpawnersLord spLord;
    public GameObject spawnerPrefab;

    void Start()
    {
        for(int x = 1; x < netMaster.netWidth - 1; x++)
        {
            SetSpawner(new OXNetPosition(x, -1), Direction.Up);
            SetSpawner(new OXNetPosition(x, netMaster.netHeight), Direction.Down);
        }

        for(int y = 1; y < netMaster.netHeight - 1; y++)
        {
            SetSpawner(new OXNetPosition(-1, y), Direction.Right);
            SetSpawner(new OXNetPosition(netMaster.netWidth, y), Direction.Left);
        }
    }

    void SetSpawner(OXNetPosition position, Direction direction)
    {
        GameObject newbie = Instantiate(spawnerPrefab);

        newbie.GetComponent<OXNetMember>().NetPosition = position;
        newbie.GetComponent<Spawner>().spawnDirection = direction;

        spLord.Register(newbie);
    }
}
