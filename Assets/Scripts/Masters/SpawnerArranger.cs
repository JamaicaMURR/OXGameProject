using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerArranger : MonoBehaviour
{
    static System.Random random = new System.Random();

    public NetMaster netMaster;
    public SpawnMaster spLord;
    public GameObject spawnerPrefab;

    void Start()
    {
        for(int x = 1; x < netMaster.netWidth - 1; x++)
        {
            SetSpawner(new NetPosition(x, -1), Direction.Up);
            SetSpawner(new NetPosition(x, netMaster.netHeight), Direction.Down);
        }

        for(int y = 1; y < netMaster.netHeight - 1; y++)
        {
            SetSpawner(new NetPosition(-1, y), Direction.Right);
            SetSpawner(new NetPosition(netMaster.netWidth, y), Direction.Left);
        }
    }

    void SetSpawner(NetPosition position, Direction direction)
    {
        GameObject newbie = Instantiate(spawnerPrefab);

        newbie.GetComponent<Transform>().Rotate(0, 0, 90 * random.Next(0, 4));
        newbie.GetComponent<NetMember>().Position = position;
        newbie.GetComponent<Spawner>().spawnDirection = direction;

        //spLord.Register(newbie);
    }
}
