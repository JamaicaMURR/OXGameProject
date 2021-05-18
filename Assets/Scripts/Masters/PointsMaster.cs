using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsMaster : MonoBehaviour
{
    int _points;
    int _dispalyPoints;

    public CentralPort central;
    public Text displayField;

    public int mergeReward = 1;
    public int progressionBonus = 1;
    public int pauserBonusPlank = 5;
    public int pauserBonus = 10;
    public int heartBonusPlank = 7;
    public int heartBonus = 25;
    public int cypherRefreshSpeed = 10;

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
        displayField.text = _dispalyPoints.ToString();

        int difference = _points - _dispalyPoints;

        if(difference > cypherRefreshSpeed)
            _dispalyPoints += difference / cypherRefreshSpeed;
        else
            _dispalyPoints = _points;
    }

    public void Reward(int totalMerged)
    {
        Points += (2 * mergeReward + progressionBonus * (totalMerged - 2)) / 2 * (totalMerged - 1); // summ of members of arithmetic progression

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

    public void RememberRecord()
    {
        int oldRec = PlayerPrefs.GetInt("record");

        if(Points > oldRec)
            PlayerPrefs.SetInt("record", Points);
    }
}
