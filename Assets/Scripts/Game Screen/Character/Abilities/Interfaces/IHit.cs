using System;

public interface IHit 
{
    HitInfo HitInfo { get; }
}

[Serializable]
public struct HitInfo
{
    public int MinDamage;
    public int MaxDamage;
}
