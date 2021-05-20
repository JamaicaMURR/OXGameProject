using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MergeMaster : MonoBehaviour
{
    public CentralPort central;

    List<GameObject> _oranges;
    List<NetPosition> _positions;

    void Awake()
    {
        _oranges = new List<GameObject>();
        _positions = new List<NetPosition>();
    }

    public void RegisterOrange(GameObject obj)
    {
        _oranges.Add(obj);

        NetMember member = obj.GetComponent<NetMember>();

        if(member != null)
            _positions.Add(member.Position);
        else
            throw new System.Exception("Given GameObject has no OXNetMember component");
    }

    public void MergeAt(NetPosition position, ref int succesCounter)
    {
        int mergingIndex = FindIndex(position);

        central.netMaster.SetDefaultState(position);

        _oranges[mergingIndex].GetComponent<OBehavior>().DieAtMerging();

        _oranges.RemoveAt(mergingIndex);
        _positions.RemoveAt(mergingIndex);

        succesCounter++;

        FindAndMerge(position, Direction.Up, ref succesCounter);
        FindAndMerge(position, Direction.Down, ref succesCounter);
        FindAndMerge(position, Direction.Left, ref succesCounter);
        FindAndMerge(position, Direction.Right, ref succesCounter);
    }

    public void MergeAt(NetPosition position)
    {
        int totalMerged = 0;

        MergeAt(position, ref totalMerged);

        central.pointsMaster.Reward(totalMerged);
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
