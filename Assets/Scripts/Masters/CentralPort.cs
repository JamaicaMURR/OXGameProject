using UnityEngine;
using System.Collections;

public class CentralPort : MonoBehaviour
{
    public NetMaster netMaster;
    public HeartsMaster heartsMaster;
    public HeartsMaster pausersMaster;
    public MergeMaster mergeMaster;
    public SpawnMaster spawnMaster;
    public PointsMaster pointsMaster;
    public FieldInputHandler inputHandler;
    public DifficultyMaster difficultyMaster;
    public MessageMaster messageMaster;
    public IndependentClocks independentClocks;
    public PopupsMaster popupsMaster;
}
