using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PointsMaster : MonoBehaviour
{
    int _dispalyPoints;

    [HideInInspector]
    public int points;

    float _timePassed;

    public CentralPort central;
    public Text displayField;

    public int mergeReward = 1;
    public int progressionBonus = 1;
    public int pauserBonusPlank = 5;
    public int heartBonusPlank = 7;

    public float cypherRefreshPeriod = 0.1f;

    public event IntEvent OnReward;
    public event IntEvent OnPausersReward;
    public event IntEvent OnHeartsReward;

    //==================================================================================================================================================================
    void Awake()
    {
        central.inputHandler.OnEscape += RememberRecord;
        central.heartsMaster.OnZeroUnits += RememberRecord;
        central.mergeMaster.AtMerged += Reward;
    }

    void Update()
    {
        _timePassed += Time.deltaTime;

        displayField.text = _dispalyPoints.ToString();

        if(_timePassed >= cypherRefreshPeriod)
        {
            if(points - _dispalyPoints != 0)
                _dispalyPoints++;

            _timePassed -= cypherRefreshPeriod;
        }
    }

    //==================================================================================================================================================================
    void Reward(int totalMerged)
    {
        int mergesToReward = totalMerged - 1; // no reward for 1 merged O

        if(mergesToReward > 0)
        {
            int rewardPoints = (int)((2 * mergeReward + progressionBonus * (mergesToReward - 1)) / 2f * mergesToReward); // summ of members of arithmetic progression

            int pausersReward = totalMerged - pauserBonusPlank + 1;
            int heartsReward = totalMerged - heartBonusPlank + 1;

            if(pausersReward > 0)
            {
                if(OnPausersReward != null)
                    OnPausersReward(central.pausersMaster.MaximalUnits - central.pausersMaster.Units);

                central.pausersMaster.Units += pausersReward;
            }

            if(heartsReward > 0)
            {
                if(OnHeartsReward != null)
                    OnHeartsReward(central.heartsMaster.MaximalUnits - central.heartsMaster.Units);

                central.heartsMaster.Units += heartsReward;
            }

            if(OnReward != null)
                OnReward(rewardPoints);

            points += rewardPoints;
        }
    }

    void RememberRecord()
    {
        int oldRec = PlayerPrefs.GetInt("record");

        if(points > oldRec)
            PlayerPrefs.SetInt("record", points);
    }
}
