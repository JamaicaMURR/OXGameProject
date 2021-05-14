using UnityEngine;
using System.Collections;

public abstract class NetMember : MonoBehaviour
{
    public NetMasterScript netMaster;
    public int netX, netY;

    /// <summary>
    /// Teleports object to position that fit it's net coordinates
    /// </summary>
    public void FitPosition()
    {
        float x, y;
        netMaster.ConvertNetXYToFieldXY(netX, netY, out x, out y);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
