using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PointsMaster : MonoBehaviour
{
    int _points;
    int _dispalyPoints;

    float _timePassed;

    public CentralPort central;
    public Text displayField;

    public int mergeReward = 1;
    public int progressionBonus = 1;
    public int pauserBonusPlank = 5;
    public int pauserBonus = 10;
    public int heartBonusPlank = 7;
    public int heartBonus = 25;

    public float cypherRefreshPeriod = 0.1f;

    public event Action OnPause;
    public event Action OnPauserUse;
    public event Action OnUnPause;
    public event Action OnReward;
    public event Action OnEscape;

    public int Points
    {
        get { return _points; }
        set
        {
            _points = value;
        }
    }

    void Awake()
    {
        central.heartsMaster.OnZeroHearts += RememberRecord;
    }

    void Update()
    {
        _timePassed += Time.deltaTime;

        displayField.text = _dispalyPoints.ToString();

        if(_timePassed >= cypherRefreshPeriod)
        {
            int difference = _points - _dispalyPoints;

            if(difference > 99)
                _dispalyPoints += 100;
            else if(difference > 9)
                _dispalyPoints += 10;
            else if(difference != 0)
                _dispalyPoints++;

            _timePassed -= cypherRefreshPeriod;
        }
    }

    public void Reward(int totalMerged)
    {
        totalMerged--; // no reward for 1 merged O

        if(totalMerged > 0)
        {
            Points += (2 * mergeReward + progressionBonus * (totalMerged - 1)) / 2 * totalMerged; // summ of members of arithmetic progression

            if(totalMerged >= pauserBonusPlank)
            {
                if(central.pausersMaster.Hearts < central.pausersMaster.MaximalHearts)
                    central.pausersMaster.Hearts++;
                else
                    Points += pauserBonus;
            }

            if(totalMerged >= heartBonusPlank)
            {
                if(central.heartsMaster.Hearts < central.heartsMaster.MaximalHearts)
                    central.heartsMaster.Hearts++;
                else
                    Points += heartBonus;
            }
        }
    }

    public void RememberRecord()
    {
        int oldRec = PlayerPrefs.GetInt("record");

        if(Points > oldRec)
            PlayerPrefs.SetInt("record", Points);
    }
}
