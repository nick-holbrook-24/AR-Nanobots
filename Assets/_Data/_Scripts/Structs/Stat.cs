using UnityEngine;

[System.Serializable]
public struct Stat
{
    [SerializeField] private uint current;
    [SerializeField] private uint max;

    public uint Current
    {
        get { return current; }
        set { current = value > max ? max : value; }
    }

    public uint Max
    {
        get { return max; }
        set { max = value > uint.MaxValue ? uint.MaxValue : value; }
    }

    public Stat(uint _current, uint _max)
    {
        current = _current;
        max = _max;
    }
}