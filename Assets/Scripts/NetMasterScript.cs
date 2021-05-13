using UnityEngine;
using Bycicles.Ranges;
using System.Collections;

public class NetMasterScript : MonoBehaviour
{
    public int netWidth = 9;
    public int netHeight = 9;

    public float pivotX = -6;
    public float pivotY = -6;

    public float cellSize = 1.5f;

    int[,] _net;

    void Start()
    {
        _net = new int[netWidth, netHeight];
    }

    /// <summary>
    /// Transform netX & netY to field coordinates
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <param name="fieldX"> X of center of cell at netX, netY </param>
    /// <param name="fieldY"> Y of center of cell at netX, netY </param>
    public void ToFieldXY(int netX, int netY, out float fieldX, out float fieldY)
    {
        fieldX = pivotX + netX * cellSize;
        fieldY = pivotY + netY * cellSize;
    }

    /// <summary>
    /// Returns state of cell at given netX and netY coordinates
    /// 0 - empty
    /// 1 - x at cell
    /// 2 - white o at cell
    /// 3 - orange o at cell
    /// 4 - out of bounds
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <returns></returns>
    public int GetCellState(int netX, int netY)
    {
        int result = 4;

        if(netX > 0 && netX < netWidth)
            if(netY > 0 && netY < netHeight)
                result = _net[netX, netY];

        return result;
    }

    /// <summary>
    /// Returns state of cell at given direction relatively to cell with given netX and netY coordinates
    /// 0 - empty
    /// 1 - x at cell
    /// 2 - white o at cell
    /// 3 - orange o at cell
    /// 4 - out of bounds
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <param name="dir"> Direction </param>
    /// <returns></returns>
    public int GetCellState(int netX, int netY, Direction dir)
    {
        int targCellX = netX;
        int targCellY = netY;

        switch(dir)
        {
            case Direction.Up:
                targCellY++;
                break;
            case Direction.Down:
                targCellY--;
                break;
            case Direction.Left:
                targCellX--;
                break;
            case Direction.Right:
                targCellX++;
                break;
        }

        return GetCellState(targCellX, targCellY);
    }

    /// <summary>
    /// Sets state to cell at given netX & netY
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <param name="state"></param>
    public void SetCellState(int netX, int netY, int state)
    {
        state.ExNotBelow(0, "State").ExNotAbove(4, "State");

        CheckNetXY(netX, netY);

        _net[netX, netY] = state;
    }

    void CheckNetXY(int netX, int netY)
    {
        netX.ExNotBelow(0, "netX").ExNotAbove(netWidth - 1, "netX");
        netY.ExNotBelow(0, "netY").ExNotBelow(netHeight - 1, "netY");
    }
}
