using System;

public interface IHeal
{
     HealInfo HealInfo { get; }
}

[Serializable]
public struct HealInfo
{
    public int MinHeal;
    public int MaxHeal;
}
