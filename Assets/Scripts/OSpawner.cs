using UnityEngine;
using System.Collections;

public class OSpawner : NetMember
{
    public GameObject spawnPrefab;
    public Direction spawnDirection;
    public OSettings settings;

    void Start()
    {
        FitPosition();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Spawn();
    }

    public void Spawn()
    {
        OScript m = Instantiate(spawnPrefab).GetComponent<OScript>();

        m.netMaster = netMaster;
        m.settings = settings;
        m.movingDirection = spawnDirection;
        m.netX = netX;
        m.netY = netY;

    }
}
