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
    public int heartBonusPlank = 7;

    public float cypherRefreshPeriod = 0.1f;

    public event Action OnReward;
    public event EventWithInt OnPausersReward;
    public event EventWithInt OnHeartsReward;

    void Awake()
    {
        central.inputHandler.OnEscape += RememberRecord;
        central.heartsMaster.OnZeroHearts += RememberRecord;
    }

    void Update()
    {
        _timePassed += Time.deltaTime;

        displayField.text = _dispalyPoints.ToString();

        if(_timePassed >= cypherRefreshPeriod)
        {
            if(_points - _dispalyPoints != 0)
                _dispalyPoints++;

            _timePassed -= cypherRefreshPeriod;
        }
    }

    public void Reward(int totalMerged)
    {
        if(OnReward != null)
            OnReward();

        int mergesToReward = totalMerged - 1; // no reward for 1 merged O

        if(mergesToReward > 0)
        {
            int rewardPoints = (int)((2 * mergeReward + progressionBonus * (mergesToReward - 1)) / 2f * mergesToReward); // summ of members of arithmetic progression

            int pausersReward = totalMerged - pauserBonusPlank + 1;
            int heartsReward = totalMerged - heartBonusPlank + 1;

            if(pausersReward > 0)
            {
                if(OnPausersReward != null)
                    OnPausersReward(central.pausersMaster.MaximalHearts - central.pausersMaster.Hearts);

                central.pausersMaster.Hearts += pausersReward;
            }

            if(heartsReward > 0)
            {
                if(OnHeartsReward != null)
                    OnHeartsReward(central.heartsMaster.MaximalHearts - central.heartsMaster.Hearts);

                central.heartsMaster.Hearts += heartsReward;
            }

            _points += rewardPoints;
        }
    }

    public void RememberRecord()
    {
        int oldRec = PlayerPrefs.GetInt("record");

        if(_points > oldRec)
            PlayerPrefs.SetInt("record", _points);
    }
}
