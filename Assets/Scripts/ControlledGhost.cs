using UnityEngine;
using System.Collections;

public class ControlledGhost : Ghost
{
    public void Locate(NetPosition position)
    {
        ghost.GetComponent<NetMember>().Position = position;
    }
}
