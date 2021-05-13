using UnityEngine;
using Bycicles.Ranges;
using System.Collections;

public class NetMasterScript : MonoBehaviour
{
    public int netWidth = 9;
    public int netHeight = 9;

    public float centerX = 0;
    public float centerY = 0;

    public float cellSize = 1.5f;

    [HideInInspector]
    public float zeroX, zeroY;

    int[,] _net;

    void Awake()
    {
        _net = new int[netWidth, netHeight];

        // calculates x & y of zero cell
        zeroX = (centerX - (netWidth / 2)) * cellSize;
        zeroY = (centerY - (netHeight / 2)) * cellSize;

        if(netWidth % 2 == 0)
            zeroX += 0.5f * cellSize;

        if(netHeight % 2 == 0)
            zeroY += 0.5f * cellSize;
    }

    void Start()
    {
    }

    /// <summary>
    /// Convert netX & netY to field coordinates
    /// </summary>
    /// <param name="netX"></param>
    /// <param name="netY"></param>
    /// <param name="fieldX"> X of center of cell at netX, netY </param>
    /// <param name="fieldY"> Y of center of cell at netX, netY </param>
    public void ConvertNetXYToFieldXY(int netX, int netY, out float fieldX, out float fieldY)
    {
        fieldX = zeroX + netX * cellSize;
        fieldY = zeroY + netY * cellSize;
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

        if(netX >= 0 && netX < netWidth)
            if(netY >= 0 && netY < netHeight)
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
        int targetCellX, targetCellY;

        GetRelativeCellNetXY(netX, netY, dir, out targetCellX, out targetCellY);

        return GetCellState(targetCellX, targetCellY);
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

    public void GetRelativeCellNetXY(int pointNetX, int pointNetY, Direction dir, out int targetNetX, out int targetNetY)
    {
        targetNetX = pointNetX;
        targetNetY = pointNetY;

        switch(dir)
        {
            case Direction.Up:
                targetNetY++;
                break;
            case Direction.Down:
                targetNetY--;
                break;
            case Direction.Left:
                targetNetX--;
                break;
            case Direction.Right:
                targetNetX++;
                break;
        }
    }

    void CheckNetXY(int netX, int netY)
    {
        netX.ExNotBelow(0, "netX").ExNotAbove(netWidth - 1, "netX");
        netY.ExNotBelow(0, "netY").ExNotAbove(netHeight - 1, "netY");
    }
}
