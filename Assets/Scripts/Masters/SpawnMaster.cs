using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnMaster : MonoBehaviour
{
    static System.Random random = new System.Random();

    float _timer = 0;

    public CentralPort central;

    public Spawner[] spawners;

    void Awake()
    {
        _timer = central.difficultyMaster.SpawnPeriod; // For imidiate first spawn
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= central.difficultyMaster.SpawnPeriod)
            Spawn();
    }

    void Spawn()
    {
        int chosenIndex = random.Next(0, spawners.Length);

        if(spawners[chosenIndex].IsReadyToSpawn())
        {
            spawners[chosenIndex].Spawn();
            _timer -= central.difficultyMaster.SpawnPeriod;
        }
    }
}
