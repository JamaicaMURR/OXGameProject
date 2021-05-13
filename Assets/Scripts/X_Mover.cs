using UnityEngine;
using System.Collections;
using System;

public class X_Mover : MonoBehaviour
{
    public NetMasterScript netMaster;

    public int netX = 0;
    public int netY = 0;

    void Start()
    {
        JumpAtCell(netX, netY); // Jump at initial position
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
    }

    /// <summary>
    /// Tries to move X at given direction
    /// </summary>
    /// <param name="dir"></param>
    void TryMove(Direction dir)
    {
        int targetNetX, targetNetY;

        netMaster.GetRelativeCellNetXY(netX, netY, dir, out targetNetX, out targetNetY);

        JumpAtCell(targetNetX, targetNetY);
    }

    /// <summary>
    /// Teleports X into cell with given coordinates if it's possible
    /// </summary>
    /// <param name="targetNetX"></param>
    /// <param name="targetNetY"></param>
    void JumpAtCell(int targetNetX, int targetNetY)
    {
        if(netMaster.GetCellState(targetNetX, targetNetY) == 0)
        {
            // Shifts to new location
            float x, y;
            netMaster.NetXYToFieldXY(targetNetX, targetNetY, out x, out y);
            transform.position = new Vector3(x, y, transform.position.z);

            // Registers at new cell in netMaster
            netMaster.SetCellState(netX, netY, 0);
            netMaster.SetCellState(targetNetX, targetNetY, 1);
            netX = targetNetX;
            netY = targetNetY;
        }
    }
}
