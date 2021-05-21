using UnityEngine;
using System.Collections;
using System;
using Bycicles.Ranges;

public class XBehavior : MonoBehaviour
{
    NetMember _netMember;
    Colorator _colorator;

    public CentralPort central;

    public event Action OnSuccefulMove;

    //============================================================================================================================================================================
    void Start()
    {
        _netMember = GetComponent<NetMember>();
        _colorator = GetComponent<Colorator>();

        if(_netMember == null)
            throw new Exception("Can't find NetMember component");

        if(_colorator == null)
            throw new Exception("Can't find Colorator component");

        central.inputHandler.OnPause += _colorator.Paint;
        central.inputHandler.OnUnPause += _colorator.Reset;

        JumpToStartPosition();
    }

    //============================================================================================================================================================================
    /// <summary>
    /// Tries to move X at given direction
    /// </summary>
    /// <param name="direction"></param>
    public void TryToMove(Direction direction)
    {
        CellState state = _netMember.GetCellState(direction);

        if(state == CellState.Empty || state == CellState.Dark)
        {
            _netMember.SetDefaultCellState();
            _netMember.JumpAt(direction);
            _netMember.SetCellState(CellState.X);

            if(OnSuccefulMove != null)
                OnSuccefulMove();
        }
    }

    //============================================================================================================================================================================
    void JumpToStartPosition()
    {
        _netMember.SetDefaultCellState();
        _netMember.JumpAt(new NetPosition(central.netMaster.netWidth / 2, central.netMaster.netHeight / 2));
        _netMember.SetCellState(CellState.X);
    }
}
