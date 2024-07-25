[System.Serializable]
public class AbilityEquipped
{
    public byte id;
    public byte level;

    [System.NonSerialized]
    public IAbility abilityScript;
}