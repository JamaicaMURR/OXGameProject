using UnityEngine;
using System.Collections;
using System;
using Bycicles.Ranges;

public class OBehavior : MonoBehaviour
{
    JustMover _mover;
    NetMember _netMember;
    SuitOrangator _orangator;
    Ghost _ghost;
    ControlledGhost _forvardGhost;
    MergeMaster _mergeMaster;

    public Direction movingDirection = Direction.Up;
    public float speed = 1;

    NetPosition _targetPosition;
    Vector3 _targetFieldPosition;

    float fixedZ;

    Action DoOnUpdate;

    //======================================================================================================================================
    void Awake()
    {
        //
        GameObject fieldMaster = GameObject.Find("FieldMaster");

        if(fieldMaster == null)
            throw new Exception("Can't find FieldMaster object");

        _mergeMaster = fieldMaster.GetComponent<MergeMaster>();

        //
        _mover = GetComponent<JustMover>();
        _netMember = GetComponent<NetMember>();
        _orangator = GetComponent<SuitOrangator>();
        _ghost = GetComponent<Ghost>();
        _forvardGhost = GetComponent<ControlledGhost>();

        if(_mover == null)
            throw new Exception("Can't find JustMover component");

        if(_netMember == null)
            throw new Exception("Cen't find OXNetMember component");

        if(_orangator == null)
            throw new Exception("Can't find SuitOrangator component");

        if(_ghost == null)
            throw new Exception("Can't find Ghost component");

        if(_forvardGhost == null)
            throw new Exception("Can't find ControlledGhost component");

        if(_mergeMaster == null)
            throw new Exception("Can't find MergeMaster");

        //
        _mover.OnMovingFinish += Arrive;

        DoOnUpdate = LookAround;
    }

    void Start()
    {
        fixedZ = transform.position.z;
    }

    void Update()
    {
        DoOnUpdate();
    }

    //======================================================================================================================================
    void LookAround()
    {
        CellState currentCS = _netMember.GetCellState();
        CellState targetCS = _netMember.GetCellState(movingDirection);

        if(currentCS == CellState.Merging)
        {
            Merge();
        }
        else if(targetCS == CellState.Empty)
        {
            SetNextDestination();
            ClaimdDestination(CellState.WhiteO);

            DoOnUpdate = Moving;
        }
        else if(targetCS == CellState.OrangeO)
        {
            SetNextDestination();
            ClaimdDestination(CellState.Merging);

            DoOnUpdate = Moving;
        }
        else if(targetCS == CellState.XRestricted)
        {
            SetNextDestination();

            DoOnUpdate = Moving;
        }
        else if(targetCS == CellState.X)
        {
            if(currentCS == CellState.XRestricted)
                Wait();
            else
                BecomeOrange();
        }
        else if(targetCS == CellState.OutOfBounds)
        {
            if(currentCS == CellState.OutOfBounds)
            {
                SetNextDestination();

                DoOnUpdate = Moving;
            }
            else
                FinishRun();
        }
        else if(targetCS == CellState.Merging)
        {
            Wait();
        }
        else if(targetCS == CellState.WhiteO)
        {
            if(currentCS == CellState.XRestricted)
                Wait();
            else
                BecomeOrange();
        }
    }

    void Moving()
    {
        _mover.Move(_targetFieldPosition, speed * Time.deltaTime);
    }

    void OrangeKemping()
    {
        // Maybe nothing at all
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

    void ClaimdDestination(CellState claim)
    {
        _forvardGhost.Locate(_netMember.GetPositionAt(movingDirection));
        _netMember.SetCellState(claim, movingDirection);
    }

    void BecomeOrange()
    {
        _netMember.SetCellState(CellState.OrangeO);
        _orangator.OrangateSuit();
        _mergeMaster.RegisterOrange(gameObject);

        DeleteGhosts();

        transform.Translate(new Vector3(0, 0, 0.5f)); // positionate object more far then others

        DoOnUpdate = OrangeKemping;
    }

    void Merge()
    {
        _mergeMaster.MergeAt(_netMember.NetPosition);

        DeleteGhosts();
        Destroy(gameObject);
    }

    void Wait()
    {
        // Maybe nothing at all
    }

    void FinishRun()
    {
        DeleteGhosts();
        Destroy(gameObject);
    }

    void DeleteGhosts()
    {
        _ghost.Delete();
        _forvardGhost.Delete();
    }
}
