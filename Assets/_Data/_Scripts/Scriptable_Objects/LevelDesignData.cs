using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDesignData", menuName = "NanoRes/LevelDesignData", order = 100)]
public class LevelDesignData : ScriptableObject
{
    public MasterAbilityList masterAbilityList;

    public event Action<NanobotData, NanobotData> OnNanobotDataChanged;

    [SerializeField] private NanobotData nanobotData;
    public NanobotData NanobotData
    {
        get => nanobotData;
        set
        {
            if (nanobotData != value)
            {
                NanobotData nanobotOldData = nanobotData;
                nanobotData = value;
                OnNanobotDataChanged?.Invoke(nanobotData, nanobotOldData);
            }
        }
    }
}