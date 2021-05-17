using UnityEngine;
using System.Collections;
using System;
using Bycicles.Ranges;

public class XBehavior : MonoBehaviour
{
    NetMember _netMember;

    //======================================================================================================================================
    void Start()
    {
        _netMember = GetComponent<NetMember>();

        if(_netMember == null)
            throw new Exception("Can't find NetMember component");

        JumpToStartPosition();
    }

    //======================================================================================================================================
    /// <summary>
    /// Tries to move X at given direction
    /// </summary>
    /// <param name="direction"></param>
    public void TryToMove(Direction direction)
    {
        CellState state = _netMember.GetCellState(direction);

        if(state == CellState.Empty)
        {
            _netMember.SetDefaultCellState();
            _netMember.JumpAt(direction);
            _netMember.SetCellState(CellState.X);
        }
    }

    void JumpToStartPosition()
    {
        _netMember.SetDefaultCellState();
        _netMember.JumpAt(new OXNetPosition(_netMember.NetWidth / 2, _netMember.NetHeight / 2));
        _netMember.SetCellState(CellState.X);
    }
}
