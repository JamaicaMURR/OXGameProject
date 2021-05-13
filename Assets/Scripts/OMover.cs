using UnityEngine;
using System.Collections;
using System;
using Bycicles.Ranges;

public class OMover : MonoBehaviour
{
    public GameObject ghostPrefab;

    public NetMasterScript netMaster;
    public OSettings settings;
    public Direction movingDirection;

    float _speed, _destinationFieldX, _destinationFieldY;

    public int _netX, _netY, _destinationNetX, _destinationNetY;

    Vector3 _destinationPoint;
    GameObject _ghost;
    Transform _ghostTransform;

    Action UpdateDeals;

    void Start()
    {
        settings.OnSpeedChange += RefreshSpeed;

        UpdateDeals = StandardMoving;

        // Ghost spawning
        _ghost = Instantiate(ghostPrefab);
        _ghostTransform = _ghost.GetComponent<Transform>();

        RefreshSpeed();
        LookAround();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDeals();
    }

    void StandardMoving()
    {
        MoveToDestination();

        if(transform.position.x == _destinationFieldX && transform.position.y == _destinationFieldY)
        {
            netMaster.SetCellState(_netX, _netY, 0);
            _netX = _destinationNetX;
            _netY = _destinationNetY;

            LookAround();
        }
    }

    void MoveToDestination()
    {
        Vector3 jump = _destinationPoint - transform.position;

        float maximalJumpLength = _speed * Time.deltaTime;

        if(jump.magnitude > maximalJumpLength)
        {
            jump.Normalize();
            jump *= maximalJumpLength;
        }

        transform.Translate(jump);
    }

    void PullGhost()
    {
        _ghostTransform.position = new Vector3(transform.position.x, transform.position.y, _ghostTransform.position.z);
    }

    void OrangeKemping()
    {

    }

    void LookAround()
    {
        PullGhost();

        if(netMaster.GetCellState(_netX, _netY) == 3)
            Merge();

        int state = netMaster.GetCellState(_netX, _netY, movingDirection);

        switch(state)
        {
            case 0:
                RefreshDestinationCoordinates();
                break;
            case 1:
                BecomeOrange();
                break;
            case 2:
                break;
            case 3:
                RefreshDestinationCoordinates();
                break;
            case 4:
                FinishRun();
                break;
        }
    }

    void RefreshDestinationCoordinates()
    {
        netMaster.GetRelativeCellNetXY(_netX, _netY, movingDirection, out _destinationNetX, out _destinationNetY);
        netMaster.ConvertNetXYToFieldXY(_destinationNetX, _destinationNetY, out _destinationFieldX, out _destinationFieldY);

        _destinationPoint = new Vector3(_destinationFieldX, _destinationFieldY, transform.position.z);

        if(netMaster.GetCellState(_destinationNetX, _destinationNetY) == 0)
            netMaster.SetCellState(_destinationNetX, _destinationNetY, 2);
    }

    void BecomeOrange()
    {
        // FixMe
        UpdateDeals = OrangeKemping;
        Destroy(_ghost);
    }

    void Merge()
    {
        // FixMe
    }


    void FinishRun()
    {
        // FixMe
        netMaster.SetCellState(_netX, _netY, 0);
        Destroy(_ghost);
        Destroy(gameObject);
    }

    void RefreshSpeed()
    {
        _speed = settings.Speed;
    }
}
