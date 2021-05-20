using UnityEngine;
using System.Collections;
using System;
using Bycicles.Ranges;

public class OBehavior : MonoBehaviour
{
    int _stepsMoved;
    float _fixedZ;

    CentralPort _central;

    JustMover _mover;
    NetMember _netMember;
    Ghost _ghost;
    ControlledGhost _forvardGhost;
    Colorator _colorator;

    public Direction movingDirection;
    public Color orange;
    public Color orangeOnPause;

    public Sprite backwardGhostSprite;
    public Sprite mergingGhostSprite;

    public float speed = 1;

    NetPosition _targetPosition;
    Vector3 _targetFieldPosition;

    Action DoOnUpdate;

    //======================================================================================================================================
    void Awake()
    {
        //
        GameObject fieldMaster = GameObject.Find("FieldMaster");

        if(fieldMaster == null)
            throw new Exception("Can't find FieldMaster object");

        _central = fieldMaster.GetComponent<CentralPort>();

        //
        _mover = GetComponent<JustMover>();
        _netMember = GetComponent<NetMember>();
        _ghost = GetComponent<Ghost>();
        _forvardGhost = GetComponent<ControlledGhost>();
        _colorator = GetComponent<Colorator>();

        if(_mover == null)
            throw new Exception("Can't find JustMover component");

        if(_netMember == null)
            throw new Exception("Cen't find OXNetMember component");

        if(_ghost == null)
            throw new Exception("Can't find Ghost component");

        if(_forvardGhost == null)
            throw new Exception("Can't find ControlledGhost component");

        if(_colorator == null)
            throw new Exception("Can't find Colorator component");

        //
        _mover.OnMovingFinish += Arrive;

        DoOnUpdate = SpawnGhosts;
    }

    void Start()
    {
        _fixedZ = transform.position.z;

        _central.inputHandler.OnPause += PaintSelf;
        _central.inputHandler.OnUnPause += UnPaintSelf;
    }

    void Update()
    {
        DoOnUpdate();
    }

    //======================================================================================================================================
    public void PaintSelf()
    {
        float exponent = 0;

        if(movingDirection == Direction.Up || movingDirection == Direction.Down)
            exponent = _stepsMoved / (float)_central.netMaster.netHeight;
        else
            exponent = _stepsMoved / (float)_central.netMaster.netWidth;

        _colorator.Paint(exponent);
    }

    public void UnPaintSelf()
    {
        _colorator.Reset();
    }

    public void DieAtMerging()
    {
        UnSubscribeAll();
        Destroy(gameObject);
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

            _forvardGhost.SetSprite(mergingGhostSprite);

            DoOnUpdate = Moving;
        }
        else if(targetCS == CellState.Dark)
        {
            if(currentCS == CellState.WhiteO)
                FinishRun();
            else
            {
                SetNextDestination();
                ClaimdDestination(CellState.DarkWithO);

                DoOnUpdate = Moving;
            }
        }
        else if(targetCS == CellState.DarkWithO)
        {
            if(currentCS == CellState.WhiteO)
                BecomeOrange();
            else
                Wait();
        }
        else if(targetCS == CellState.X)
        {
            if(currentCS == CellState.DarkWithO || currentCS == CellState.OutOfBounds)
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
                throw new Exception("O going out if bounds");
        }
        else if(targetCS == CellState.Merging)
        {
            Wait();
        }
        else if(targetCS == CellState.WhiteO)
        {
            if(currentCS == CellState.DarkWithO)
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
        _stepsMoved++;

        _netMember.SetDefaultCellState();
        _netMember.NetPosition = _targetPosition;

        _ghost.Pull();

        DoOnUpdate = LookAround;
    }

    void SetNextDestination()
    {
        _targetPosition = _netMember.GetPositionAt(movingDirection);

        Vector2 fieldPoint = _netMember.ConvertPosition(_targetPosition);
        _targetFieldPosition = new Vector3(fieldPoint.x, fieldPoint.y, _fixedZ);
    }

    void ClaimdDestination(CellState claim)
    {
        _forvardGhost.Locate(_netMember.GetPositionAt(movingDirection));
        _netMember.SetCellState(claim, movingDirection);
    }

    void BecomeOrange()
    {
        _netMember.SetCellState(CellState.OrangeO);
        _central.mergeMaster.RegisterOrange(gameObject);

        UnSubscribeAll();

        _colorator.initialColor = orange;
        _colorator.targetColor = orangeOnPause;

        _colorator.Reset();

        _central.inputHandler.OnPause += _colorator.Paint;
        _central.inputHandler.OnUnPause += _colorator.Reset;

        DeleteGhosts();

        transform.Translate(new Vector3(0, 0, 0.25f)); // positionate object more far then others

        DoOnUpdate = OrangeKemping;
    }

    void Merge()
    {
        _central.mergeMaster.MergeAt(_netMember.NetPosition);

        DeleteGhosts();
        UnSubscribeAll();
        Destroy(gameObject);
    }

    void Wait()
    {
        // Maybe nothing at all
    }

    void FinishRun()
    {
        _central.heartsMaster.Units--;
        _netMember.SetDefaultCellState();

        DeleteGhosts();
        UnSubscribeAll();
        Destroy(gameObject);
    }

    void SpawnGhosts()
    {
        _ghost.Spawn();
        _ghost.SetSprite(backwardGhostSprite);

        _forvardGhost.Spawn();
        _forvardGhost.CloneSprite();

        DoOnUpdate = LookAround;
    }

    void DeleteGhosts()
    {
        _ghost.Delete();
        _forvardGhost.Delete();
    }

    void UnSubscribeAll()
    {
        _central.inputHandler.OnPause -= PaintSelf;
        _central.inputHandler.OnUnPause -= UnPaintSelf;

        _central.inputHandler.OnPause -= _colorator.Paint;
        _central.inputHandler.OnUnPause -= _colorator.Reset;
    }
}
