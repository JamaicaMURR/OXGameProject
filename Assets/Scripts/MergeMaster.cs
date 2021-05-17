using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MergeMaster : MonoBehaviour
{
    public NetMaster netMaster;

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
            _positions.Add(member.NetPosition);
        else
            throw new System.Exception("Given GameObject has no OXNetMember component");
    }

    public void MergeAt(NetPosition position)
    {
        int mergingIndex = FindIndex(position);

        netMaster.SetDefaultState(position);

        Destroy(_oranges[mergingIndex]);

        _oranges.RemoveAt(mergingIndex);
        _positions.RemoveAt(mergingIndex);

        FindAndMerge(position, Direction.Up);
        FindAndMerge(position, Direction.Down);
        FindAndMerge(position, Direction.Left);
        FindAndMerge(position, Direction.Right);
    }

    void FindAndMerge(NetPosition position, Direction direction)
    {
        NetPosition relativePosition = netMaster.GetRelativePosition(position, direction);

        if(netMaster.GetCellState(relativePosition) == CellState.OrangeO)
            MergeAt(relativePosition);

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
