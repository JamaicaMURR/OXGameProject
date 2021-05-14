using UnityEngine;
using System.Collections;

public class Cell
{
    CellState _defaultState;
    CellState _actualState;

    public CellState State
    {
        get { return _actualState; }
        set { _actualState = value; }
    }

    //======================================================================================================================================
    public Cell(CellState defaultState)
    {
        _defaultState = defaultState;
        SetDefault();
    }

    public Cell() : this(CellState.Empty) { }

    //======================================================================================================================================
    public void SetDefault()
    {
        State = _defaultState;
    }

}
