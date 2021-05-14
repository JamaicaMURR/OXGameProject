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
    Orangator _orangator;
    GameObject _ghost;
    Transform _ghostTransform;

    Action UpdateDeals;

    void Start()
    {
        _orangator = gameObject.GetComponent<Orangator>();

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

        if(netMaster.GetCellState(_netX, _netY) == CellState.OrangeO)
            Merge();

        CellState state = netMaster.GetCellState(_netX, _netY, movingDirection);

        switch(state)
        {
            case CellState.Empty:
                RefreshDestinationCoordinates();
                netMaster.SetCellState(_destinationNetX, _destinationNetY, CellState.WhiteO);
                break;
            case CellState.X:
                BecomeOrange();
                break;
            case CellState.WhiteO:
                break;
            case CellState.OrangeO:
                RefreshDestinationCoordinates();
                break;
            case CellState.OutOfBounds:
                FinishRun();
                break;
        }
    }

    void RefreshDestinationCoordinates()
    {
        netMaster.GetRelativeCellNetXY(_netX, _netY, movingDirection, out _destinationNetX, out _destinationNetY);
        netMaster.ConvertNetXYToFieldXY(_destinationNetX, _destinationNetY, out _destinationFieldX, out _destinationFieldY);

        _destinationPoint = new Vector3(_destinationFieldX, _destinationFieldY, transform.position.z);
    }

    void BecomeOrange()
    {
        _orangator.SwitchSprite();
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
