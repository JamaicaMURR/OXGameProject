using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpawnMaster : MonoBehaviour
{
    static System.Random random = new System.Random();

    float _timer = 0;

    int _whitesOnField;

    public CentralPort central;

    public Spawner[] spawners;

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public int WhitesOnField { get { return _whitesOnField; } }

    //==================================================================================================================================================================
    void Awake()
    {
        _timer = central.difficultyMaster.SpawnPeriod; // For imidiate first spawn

        foreach(Spawner s in spawners)
            s.OnRelease += delegate () { _whitesOnField++; }; // Each released O must be counted

        central.mergeMaster.OnOrangeRegister += delegate () { _whitesOnField--; };
        central.mergeMaster.AtMerged += delegate (int i) { _whitesOnField--; }; // Every merging eats one O that activate merging
    }

    //==================================================================================================================================================================
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= central.difficultyMaster.SpawnPeriod)
            Spawn();
    }

    //==================================================================================================================================================================
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
