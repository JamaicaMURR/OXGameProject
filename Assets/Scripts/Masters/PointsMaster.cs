using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsMaster : MonoBehaviour
{
    int _points;
    int _dispalyPoints;

    public Text displayField;

    public int cypherRefreshSpeed = 10;

    public int Points
    {
        get { return _points; }
        set
        {
            _points = value;
        }
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

}
