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

    void Awake()
    {
        _spawners = new List<Spawner>();
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
        _spawners[random.Next(0, _spawners.Count)].Spawn();
    }
}
