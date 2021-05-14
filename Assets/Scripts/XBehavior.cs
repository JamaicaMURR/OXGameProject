using UnityEngine;
using System.Collections;
using System;

public class XBehavior : MonoBehaviour
{
    OXNetMember _netMember;

    //======================================================================================================================================
    void Start()
    {
        _netMember = GetComponent<OXNetMember>();

        if(_netMember == null)
            throw new Exception("Can't find NetMember component");

        JumpToStartPosition();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            TryMove(Direction.Up);

        if(Input.GetKeyDown(KeyCode.S))
            TryMove(Direction.Down);

        if(Input.GetKeyDown(KeyCode.A))
            TryMove(Direction.Left);

        if(Input.GetKeyDown(KeyCode.D))
            TryMove(Direction.Right);

        if(Input.GetKeyDown(KeyCode.Space))
            JumpToStartPosition();
    }

    //======================================================================================================================================
    /// <summary>
    /// Tries to move X at given direction
    /// </summary>
    /// <param name="direction"></param>
    void TryMove(Direction direction)
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
