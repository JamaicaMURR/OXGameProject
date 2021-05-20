using UnityEngine;
using System.Collections;
using System;

public class Spawner : MonoBehaviour
{
    NetMember _netMember;
    GameObject _newbie;

    public GameObject spawnPrefab;
    public Direction spawnDirection;

    public SpriteAnimator foldingScreenAnimator;

    public float prespawningTime = 0.5f;

    [HideInInspector]
    public bool isSpawning = false;

    void Awake()
    {
        _netMember = GetComponent<NetMember>();

        if(_netMember == null)
            throw new Exception("Can't find OXNetMember component");

        foldingScreenAnimator.animationTime = prespawningTime;
        foldingScreenAnimator.OnAnimationEnd += Release;
    }

    public bool IsReadyToSpawn()
    {
        return _netMember.GetCellState(spawnDirection) == CellState.Dark && !isSpawning;
    }

    public void Spawn()
    {
        if(IsReadyToSpawn())
        {
            _newbie = Instantiate(spawnPrefab);

            _newbie.GetComponent<NetMember>().Position = _netMember.Position;
            _newbie.GetComponent<OBehavior>().movingDirection = spawnDirection;

            foldingScreenAnimator.StartAnimation();

            isSpawning = true;
        }
    }

    void Release()
    {
        _newbie.GetComponent<OBehavior>().enabled = true;

        isSpawning = false; // To early, because SpawnMaster can order to spawn before newbie O start moving, however, on big spawn periods, this issue is not exists.
    }
}
