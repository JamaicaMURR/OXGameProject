using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Provides to object ability to be a member of OXnet
/// </summary>
public class NetMember : MonoBehaviour
{
    OXNetPosition _netPosition;

    [SerializeField]
    NetMaster netMaster;

    public OXNetPosition NetPosition
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

    //======================================================================================================================================
    /// <summary>
    /// Sets object on field on fitted given OXnet position
    /// </summary>
    public void JumpAt(OXNetPosition position)
    {
        Vector2 fieldPoint = ConvertPosition(position);
        transform.position = new Vector3(fieldPoint.x, fieldPoint.y, transform.position.z);

        _netPosition = position;
    }

    public void JumpAt(Direction direction)
    {
        JumpAt(netMaster.GetRelativePosition(NetPosition, direction));
    }

    public Vector2 ConvertPosition(OXNetPosition position)
    {
        return netMaster.Convert(position);
    }

    public CellState GetCellState()
    {
        return netMaster.GetCellState(NetPosition);
    }

    public CellState GetCellState(Direction direction)
    {
        return netMaster.GetCellState(netMaster.GetRelativePosition(NetPosition, direction));
    }

    public OXNetPosition GetPositionAt(Direction direction)
    {
        return netMaster.GetRelativePosition(NetPosition, direction);
    }

    public void SetCellState(CellState state)
    {
        netMaster.SetCellState(NetPosition, state);
    }

    public void SetCellState(CellState state, Direction direction)
    {
        netMaster.SetCellState(netMaster.GetRelativePosition(NetPosition, direction), state);
    }

    public void SetDefaultCellState()
    {
        netMaster.SetDefaultState(NetPosition);
    }

}
