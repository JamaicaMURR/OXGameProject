using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnMaster : MonoBehaviour
{
    static System.Random random = new System.Random();

    public float spawnPeriod = 1;

    float _timer = 0;

    public Spawner[] spawners;

    List<Spawner> _availableSpawners;

    void Awake()
    {
        _availableSpawners = new List<Spawner>(spawners);

        _timer = spawnPeriod;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= spawnPeriod)            
            Spawn();
    }

    void Spawn()
    {
        if(_availableSpawners.Count == 0)
            _availableSpawners = new List<Spawner>(spawners);

        int chosenIndex = random.Next(0, _availableSpawners.Count);

        if(_availableSpawners[chosenIndex].IsReadyToSpawn())
        {
            _availableSpawners[chosenIndex].Spawn();
            _timer -= spawnPeriod;
        }

        _availableSpawners.RemoveAt(chosenIndex);
    }
}
