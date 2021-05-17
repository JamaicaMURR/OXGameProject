using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnersLord : MonoBehaviour
{
    static System.Random random = new System.Random();

    public float spawnPeriod = 1;

    float _timer = 0;

    List<Spawner> _spawners;
    List<Spawner> _availableSpawners;

    void Awake()
    {
        _spawners = new List<Spawner>();
        _availableSpawners = new List<Spawner>(_spawners);

        _timer = spawnPeriod;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= spawnPeriod)
        {
            _timer -= spawnPeriod;
            Spawn();
        }
    }

    public void Register(GameObject spawner)
    {
        _spawners.Add(spawner.GetComponent<Spawner>());
    }

    public void Spawn()
    {
        if(_availableSpawners.Count == 0)
            _availableSpawners = new List<Spawner>(_spawners);

        int chosenIndex = random.Next(0, _availableSpawners.Count);

        _availableSpawners[chosenIndex].Spawn();
        _availableSpawners.RemoveAt(chosenIndex);
    }
}
