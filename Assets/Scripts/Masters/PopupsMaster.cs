using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupsMaster : MonoBehaviour
{
    public CentralPort central;

    public PopUpSpawner pointsPopupSpawner;
    public PopUpSpawner heartsPopupSpawner;
    public PopUpSpawner pauserPopupSpawner;

    private void Awake()
    {
        central.pointsMaster.OnReward += delegate (int i) { pointsPopupSpawner.Pop("+" + i); };
        central.pointsMaster.OnHeartsReward += delegate (int i) { heartsPopupSpawner.Pop("+" + i); };
        central.pointsMaster.OnPausersReward += delegate (int i) { pauserPopupSpawner.Pop("+" + i); };

        central.heartsMaster.OnUnitLost += delegate () { heartsPopupSpawner.Pop("-1"); };
        central.pausersMaster.OnUnitLost += delegate () { pauserPopupSpawner.Pop("-1"); };
    }
}
