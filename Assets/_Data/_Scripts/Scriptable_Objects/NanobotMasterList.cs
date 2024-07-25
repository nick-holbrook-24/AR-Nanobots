using UnityEngine;

[CreateAssetMenu(fileName = "MasterNanobotList", menuName = "NanoRes/MasterNanobotList", order = 1)]
public class MasterNanobotList : ScriptableObject
{
    public NanobotData[] nanobots;
}