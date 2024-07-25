using UnityEngine;

[CreateAssetMenu(fileName = "MasterAbilityList", menuName = "NanoRes/MasterAbilityList", order = 101)]
public class MasterAbilityList : ScriptableObject
{
    public AbilityDefinition[] abilities;
}