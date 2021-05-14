using UnityEngine;
using System.Collections;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Direction spawnDirection;

    public int netX, netY;

    OXNetMember _netMember;
    OXNetPosition _spawnerPosition;

    void Awake()
    {
        _netMember = GetComponent<OXNetMember>();

        if(_netMember == null)
            throw new Exception("Can't find OXNetMember component");

        _spawnerPosition = new OXNetPosition(netX, netY);
    }

    void Start()
    {
        _netMember.JumpAt(new OXNetPosition(netX, netY));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // Just for test!
            Spawn();
    }

    public void Spawn()
    {
        GameObject newbie = Instantiate(spawnPrefab);

        newbie.GetComponent<OXNetMember>().NetPosition = _spawnerPosition;
        newbie.GetComponent<OBehavior>().movingDirection = spawnDirection;
    }
}
