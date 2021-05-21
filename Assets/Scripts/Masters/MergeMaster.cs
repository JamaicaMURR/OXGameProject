using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MergeMaster : MonoBehaviour
{
    public CentralPort central;

    public event IntEvent AtMerged;
    public event Action OnOrangeRegister;

    public int OrangesCount { get { return _oranges.Count; } }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    List<OBehavior> _oranges;
    List<NetPosition> _positions; // Oranges and their positions stores separately for more redable code (and optimisation)

    //==================================================================================================================================================================
    void Awake()
    {
        _oranges = new List<OBehavior>();
        _positions = new List<NetPosition>();
    }

    //==================================================================================================================================================================
    public void RegisterOrange(GameObject obj)
    {
        _oranges.Add(obj.GetComponent<OBehavior>());
        _positions.Add(obj.GetComponent<NetMember>().Position);

        if(OnOrangeRegister != null)
            OnOrangeRegister();
    }

    public void MergeAt(NetPosition position)
    {
        int totalMerged = 0;

        MergeAt(position, ref totalMerged);

        if(AtMerged != null)
            AtMerged(totalMerged);
    }

    void MergeAt(NetPosition position, ref int succesCounter)
    {
        int mergingIndex = FindIndex(position);

        central.netMaster.SetDefaultState(position);

        _oranges[mergingIndex].DieAtMerging();

        _oranges.RemoveAt(mergingIndex);
        _positions.RemoveAt(mergingIndex);

        succesCounter++;

        FindAndMerge(position, Direction.Up, ref succesCounter);
        FindAndMerge(position, Direction.Down, ref succesCounter);
        FindAndMerge(position, Direction.Left, ref succesCounter);
        FindAndMerge(position, Direction.Right, ref succesCounter);
    }

    void FindAndMerge(NetPosition position, Direction direction, ref int succesCounter)
    {
        NetPosition relativePosition = central.netMaster.GetRelativePosition(position, direction);

        if(central.netMaster.GetCellState(relativePosition) == CellState.OrangeO)
            MergeAt(relativePosition, ref succesCounter);
    }

    int FindIndex(NetPosition position)
    {
        int result = -1;

        for(int i = 0; i < _positions.Count; i++)
        {
            if(position.X == _positions[i].X && position.Y == _positions[i].Y)
            {
                result = i;
                break;
            }
        }

        if(result == -1)
            throw new System.Exception("Unregisterd merging");

        return result;
    }

}
