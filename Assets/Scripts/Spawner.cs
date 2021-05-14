using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Direction spawnDirection;
    public OSettings settings;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Spawn();
    }

    public void Spawn()
    {
        OBehavior m = Instantiate(spawnPrefab).GetComponent<OBehavior>();

        //m.netMaster = netMaster;
        //m.settings = settings;
        //m.movingDirection = spawnDirection;
        //m.netX = netX;
        //m.netY = netY;
    }
}
