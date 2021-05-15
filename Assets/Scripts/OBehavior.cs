using UnityEngine;
using System.Collections;
using System;
using Bycicles.Ranges;

public class OBehavior : MonoBehaviour
{
    JustMover _mover;
    OXNetMember _netMember;
    SuitOrangator _orangator;
    Ghost _ghost;

    public Direction movingDirection = Direction.Up;
    public float speed = 1;

    OXNetPosition _targetPosition;
    Vector3 _targetFieldPosition;

    float fixedZ;

    Action DoOnUpdate;

    //======================================================================================================================================
    void Awake()
    {
        _mover = GetComponent<JustMover>();
        _netMember = GetComponent<OXNetMember>();
        _orangator = GetComponent<SuitOrangator>();
        _ghost = GetComponent<Ghost>();

        if(_mover == null)
            throw new Exception("Can't find JustMover component");

        if(_netMember == null)
            throw new Exception("Cen't find OXNetMember component");

        if(_orangator == null)
            throw new Exception("Can't find SuitOrangator component");

        if(_ghost == null)
            throw new Exception("Can't find Ghost component");

        _mover.OnMovingFinish += Arrive;

        DoOnUpdate = LookAround;
    }

    void Start()
    {
        fixedZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        DoOnUpdate();
    }

    //======================================================================================================================================
    void Moving()
    {
        _mover.Move(_targetFieldPosition, speed * Time.deltaTime);
    }

    void OrangeKemping()
    {

    }

    void LookAround()
    {
        CellState currentCellState = _netMember.GetCellState();
        CellState targetCellState = _netMember.GetCellState(movingDirection);

        if(currentCellState == CellState.OrangeO)
        {
            Merge();
        }
        else if(targetCellState == CellState.Empty)
        {
            SetNextDestination();
            ClaimdDestination();

            DoOnUpdate = Moving;
        }
        else if(targetCellState == CellState.OrangeO || targetCellState == CellState.XRestricted)
        {
            SetNextDestination();

            DoOnUpdate = Moving;
        }
        else if(targetCellState == CellState.X)
        {
            if(currentCellState == CellState.XRestricted)
                Wait();
            else
                BecomeOrange();
        }
        else if(targetCellState == CellState.OutOfBounds)
        {
            if(currentCellState == CellState.OutOfBounds)
            {
                SetNextDestination();

                DoOnUpdate = Moving;
            }
            else
                FinishRun();
        }
        else if(targetCellState == CellState.WhiteO)
        {
            if(currentCellState == CellState.XRestricted)
                Wait();
            else
                BecomeOrange();
        }
    }

    void Arrive()
    {
        _netMember.SetDefaultCellState();
        _netMember.NetPosition = _targetPosition;

        _ghost.Pull();

        DoOnUpdate = LookAround;
    }

    void SetNextDestination()
    {
        _targetPosition = _netMember.GetPositionAt(movingDirection);

        Vector2 fieldPoint = _netMember.ConvertPosition(_targetPosition);
        _targetFieldPosition = new Vector3(fieldPoint.x, fieldPoint.y, fixedZ);
    }

    void ClaimdDestination()
    {
        _netMember.SetCellState(CellState.WhiteO, movingDirection);
    }

    void BecomeOrange()
    {
        _netMember.SetCellState(CellState.OrangeO);
        _orangator.OrangateSuit();
        _ghost.Delete();

        DoOnUpdate = OrangeKemping;
    }

    void Merge()
    {
        _ghost.Delete();

        Destroy(gameObject);
    }

    void Wait()
    {

    }

    void FinishRun()
    {
        _ghost.Delete();

        Destroy(gameObject);
    }
}
