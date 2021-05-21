using Bycicles.Ranges;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMaster : MonoBehaviour
{
    float _averSpawnPeriod;

    float AverSpawnPeriod { get { return _averSpawnPeriod; } set { _averSpawnPeriod = value.NotBelow(minimalAverSpawnPeriod).NotAbove(initialAverSpawnPeriod); } }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public CentralPort central;

    public float initialAverSpawnPeriod = 2;
    public float minimalAverSpawnPeriod = 0.5f;
    public float decreasingSpawnPeriodStep = 0.05f;

    public float oDefaultSpeed = 0.75f;
    public float oOnDarkCellSpeed = 2;
    public float oMergeDashSpeed = 5;

    public float SpawnPeriod { get { return CalculateSpawnPeriod(); } }

    //==================================================================================================================================================================
    private void Awake()
    {
        _averSpawnPeriod = initialAverSpawnPeriod;

        central.mergeMaster.AtMerged += IncreaseDifficulty; // Simple model of difficulty rising: difficulty rises at each merged O
    }

    //==================================================================================================================================================================
    void IncreaseDifficulty(int power)
    {
        AverSpawnPeriod -= power * decreasingSpawnPeriodStep;
    }

    float CalculateSpawnPeriod()
    {
        return AverSpawnPeriod;
    }
}
