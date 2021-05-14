using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetArrangerScript : MonoBehaviour
{
    public OXNetMaster netMaster;

    public GameObject line;

    void Start()
    {
        List<float> xLines = new List<float>();
        List<float> yLines = new List<float>();

        float halfCellSize = 0.5f * netMaster.cellSize;

        float x = netMaster.zeroX - halfCellSize;
        float y = netMaster.zeroY - halfCellSize;

        for(int i = 0; i <= netMaster.netWidth; i++)
        {
            xLines.Add(x);
            x += netMaster.cellSize;
        }

        for(int i = 0; i <= netMaster.netHeight; i++)
        {
            yLines.Add(y);
            y += netMaster.cellSize;
        }

        float zpos = line.GetComponent<Transform>().position.z;

        for(int i = 0; i < xLines.Count; i++)
            for(int j = 0; j < yLines.Count - 1; j++)
            {
                GameObject newbie = Instantiate(line);
                newbie.GetComponent<Transform>().position = new Vector3(xLines[i], yLines[j] + halfCellSize, zpos);
            }

        for(int j = 0; j < yLines.Count; j++)
            for(int i = 0; i < xLines.Count - 1; i++)
            {
                GameObject newbie = Instantiate(line);
                Transform nt = newbie.GetComponent<Transform>();
                nt.rotation = new Quaternion(0, 0, 1, 1);
                nt.position = new Vector3(xLines[i] + halfCellSize, yLines[j], zpos);
            }
    }
}
