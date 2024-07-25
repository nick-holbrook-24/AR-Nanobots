using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NanobotData", menuName = "NanoRes/NanobotData", order = 2)]
public class NanobotData : ScriptableObject
{
    public string botName;
    public GameObject prefab;
    public Material material;
    public Vector3 scale;
    public Stat energy;
    public Stat cleanliness;
    public AbilityEquipped[] abilities;

    [NonSerialized]
    public bool isJumping;

    public event Action OnDataChanged;
    private bool dataChanged;

    public bool DataChanged
    {
        get { return dataChanged; }
        set
        {
            dataChanged = value;

            if (dataChanged)
            {
                OnDataChanged?.Invoke();
                dataChanged = false;
            }
        }
    }
}