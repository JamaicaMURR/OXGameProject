﻿using UnityEngine;
using Bycicles.Ranges;
using System.Collections;
using System.Collections.Generic;

public class NetMaster : MonoBehaviour
{
    Cell[,] _net;

    public int netWidth = 9;
    public int netHeight = 9;

    public float centerX = 0;
    public float centerY = 0;

    public float cellSize = 1.5f;

    public float zeroX, zeroY;

    void Awake()
    {
        _net = new Cell[netWidth, netHeight];

        for(int x = 0; x < netWidth; x++)
            for(int y = 0; y < netHeight; y++)
            {
                // Field border cells get default state "Dark" 
                if(x == 0 || y == 0 || x == netWidth - 1 || y == netHeight - 1)
                    _net[x, y] = new Cell(CellState.Dark);
                else
                    _net[x, y] = new Cell(CellState.Empty);
            }

        // calculates x & y of zero cell
        zeroX = (centerX - (netWidth / 2)) * cellSize;
        zeroY = (centerY - (netHeight / 2)) * cellSize;

        if(netWidth % 2 == 0)
            zeroX += 0.5f * cellSize;

        if(netHeight % 2 == 0)
            zeroY += 0.5f * cellSize;
    }

    /// <summary>
    /// Converts OXnet position to field coordinates
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector2 Convert(NetPosition position)
    {
        float x = zeroX + position.X * cellSize;
        float y = zeroY + position.Y * cellSize;

        return new Vector2(x, y);
    }

    /// <summary>
    /// Returns position of cell at given direction relative given position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public NetPosition GetRelativePosition(NetPosition position, Direction direction)
    {
        int resultX = position.X;
        int resultY = position.Y;

        switch(direction)
        {
            case Direction.Up:
                resultY++;
                break;
            case Direction.Down:
                resultY--;
                break;
            case Direction.Left:
                resultX--;
                break;
            case Direction.Right:
                resultX++;
                break;
        }

        return new NetPosition(resultX, resultY);
    }

    /// <summary>
    /// Returns state of cell at given netX and netY coordinates
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <returns></returns>
    public CellState GetCellState(NetPosition position)
    {
        CellState result = CellState.OutOfBounds;

        if(position.X >= 0 && position.X < netWidth)
            if(position.Y >= 0 && position.Y < netHeight)
                result = _net[position.X, position.Y].State;

        return result;
    }

    /// <summary>
    /// Returns state of cell at given direction relatively to cell with given netX and netY coordinates
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <param name="dir"> Direction </param>
    /// <returns></returns>
    public CellState GetCellState(NetPosition position, Direction direction)
    {
        return GetCellState(GetRelativePosition(position, direction));
    }

    /// <summary>
    /// Returns defaultState of cell at given position;
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public CellState GetDefCellState(NetPosition position)
    {
        CellState result = CellState.OutOfBounds;

        if(position.X >= 0 && position.X < netWidth)
            if(position.Y >= 0 && position.Y < netHeight)
                result = _net[position.X, position.Y].DefaultState;

        return result;
    }

    /// <summary>
    /// Sets state to cell at given netX & netY
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <param name="state"></param>
    public void SetCellState(NetPosition position, CellState state)
    {
        if(position.X >= 0 && position.X < netWidth)
            if(position.Y >= 0 && position.Y < netHeight)
                _net[position.X, position.Y].State = state;
    }

    public void SetDefaultState(NetPosition position)
    {
        if(position.X >= 0 && position.X < netWidth)
            if(position.Y >= 0 && position.Y < netHeight)
                _net[position.X, position.Y].SetDefault();
    }
}
