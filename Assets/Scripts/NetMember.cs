using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Provides to object ability to be a member of OXnet
/// </summary>
public class NetMember : MonoBehaviour
{
    NetPosition _netPosition;

    [SerializeField]
    NetMaster netMaster;

    public bool useInitialPositon = false;
    public int x = 0;
    public int y = 0;

    public NetPosition Position
    {
        get { return _netPosition; }
        set { JumpAt(value); }
    }

    public int NetWidth { get { return netMaster.netWidth; } }
    public int NetHeight { get { return netMaster.netHeight; } }

    //======================================================================================================================================
    void Awake()
    {
        if(netMaster == null)
        {
            GameObject fieldMaster = GameObject.Find("FieldMaster");

            if(fieldMaster == null)
                throw new Exception("Can't find FieldMaster object");

            netMaster = fieldMaster.GetComponent<NetMaster>();

            if(netMaster == null)
                throw new Exception("Can't find NetMaster component in FieldMaster");
        }

    }

    private void Start()
    {
        if(useInitialPositon)
            Position = new NetPosition(x, y);
    }

    //======================================================================================================================================
    /// <summary>
    /// Sets object on field on fitted given OXnet position
    /// </summary>
    public void JumpAt(NetPosition position)
    {
        Vector2 fieldPoint = ConvertPosition(position);
        transform.position = new Vector3(fieldPoint.x, fieldPoint.y, transform.position.z);

        _netPosition = position;
    }

    public void JumpAt(Direction direction)
    {
        JumpAt(netMaster.GetRelativePosition(Position, direction));
    }

    public Vector2 ConvertPosition(NetPosition position)
    {
        return netMaster.Convert(position);
    }

    public CellState GetCellState()
    {
        return netMaster.GetCellState(Position);
    }

    public CellState GetCellState(Direction direction)
    {
        return netMaster.GetCellState(netMaster.GetRelativePosition(Position, direction));
    }

    public NetPosition GetPositionAt(Direction direction)
    {
        return netMaster.GetRelativePosition(Position, direction);
    }

    public void SetCellState(CellState state)
    {
        netMaster.SetCellState(Position, state);
    }

    public void SetCellState(CellState state, Direction direction)
    {
        netMaster.SetCellState(netMaster.GetRelativePosition(Position, direction), state);
    }

    public void SetDefaultCellState()
    {
        netMaster.SetDefaultState(Position);
    }

}
