using UnityEngine;
using System.Collections;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Direction spawnDirection;

    NetMember _netMember;

    void Awake()
    {
        _netMember = GetComponent<NetMember>();

        if(_netMember == null)
            throw new Exception("Can't find OXNetMember component");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // Just for test!
            Spawn();
    }

    public void Spawn()
    {
        GameObject newbie = Instantiate(spawnPrefab);

        newbie.GetComponent<NetMember>().NetPosition = _netMember.NetPosition;
        newbie.GetComponent<OBehavior>().movingDirection = spawnDirection;
    }
}
