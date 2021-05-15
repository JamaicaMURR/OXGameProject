using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MergeMaster : MonoBehaviour
{
    public OXNetMaster netMaster;

    List<GameObject> _oranges;
    List<OXNetPosition> _positions;

    void Awake()
    {
        _oranges = new List<GameObject>();
        _positions = new List<OXNetPosition>();
    }

    public void RegisterOrange(GameObject obj)
    {
        _oranges.Add(obj);

        OXNetMember member = obj.GetComponent<OXNetMember>();

        if(member != null)
            _positions.Add(member.NetPosition);
        else
            throw new System.Exception("Given GameObject has no OXNetMember component");
    }

    public void MergeAt(OXNetPosition position)
    {
        int mergingIndex = FindIndex(position);

        Destroy(_oranges[mergingIndex]);

        Debug.Log("Merging"); // place for more code!<=============================================================!!!!

        _oranges.RemoveAt(mergingIndex);
        _positions.RemoveAt(mergingIndex);
    }

    int FindIndex(OXNetPosition position)
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
