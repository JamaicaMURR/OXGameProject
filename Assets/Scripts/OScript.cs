using UnityEngine;
using System.Collections;
using System;
using Bycicles.Ranges;

public class OScript : NetMember
{
    public GameObject ghostPrefab;
    public OSettings settings;
    public Direction movingDirection;

    float _speed, _destinationFieldX, _destinationFieldY;

    public int destinationNetX, destinationNetY;

    Vector3 _destinationPoint;
    Orangator _orangator;
    GameObject _ghost;
    Transform _ghostTransform;

    Action UpdateDeals;

    void Start()
    {
        FitPosition();

        _orangator = gameObject.GetComponent<Orangator>();

        settings.OnSpeedChange += RefreshSpeed;

        UpdateDeals = StandardMoving;

        // Ghost spawning
        _ghost = Instantiate(ghostPrefab);
        _ghostTransform = _ghost.GetComponent<Transform>();

        //
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
            netMaster.SetCellState(netX, netY, CellState.Empty);
            netX = destinationNetX;
            netY = destinationNetY;

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

        if(netMaster.GetCellState(netX, netY) == CellState.OrangeO)
            Merge();

        CellState state = netMaster.GetCellState(netX, netY, movingDirection);

        switch(state)
        {
            case CellState.Empty:
                SetNextDestination();
                netMaster.SetCellState(destinationNetX, destinationNetY, CellState.WhiteO);
                break;
            case CellState.X:
                BecomeOrange();
                break;
            case CellState.WhiteO:
                break;
            case CellState.OrangeO:
                SetNextDestination();
                break;
            case CellState.OutOfBounds:
                FinishRun();
                break;
        }
    }

    void SetNextDestination()
    {
        netMaster.GetRelativeCellNetXY(netX, netY, movingDirection, out destinationNetX, out destinationNetY);
        netMaster.ConvertNetXYToFieldXY(destinationNetX, destinationNetY, out _destinationFieldX, out _destinationFieldY);

        _destinationPoint = new Vector3(_destinationFieldX, _destinationFieldY, transform.position.z);
    }

    void BecomeOrange()
    {
        _orangator.SwitchSprite();
        UpdateDeals = OrangeKemping;
        netMaster.SetCellState(netX, netY, CellState.OrangeO);
        Destroy(_ghost);
    }

    void Merge()
    {
        Destroy(_ghost);
        Destroy(gameObject);
    }


    void FinishRun()
    {
        // FixMe
        netMaster.SetCellState(netX, netY, 0);
        Destroy(_ghost);
        Destroy(gameObject);
    }

    void RefreshSpeed()
    {
        _speed = settings.Speed;
    }
}
