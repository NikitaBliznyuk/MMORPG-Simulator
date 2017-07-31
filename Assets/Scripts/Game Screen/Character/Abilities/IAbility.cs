using System;
using System.Collections;
using Game.Character;

public interface IAbility
{
    AbilityInfo AbilityInfo { get; }
    void Invoke(CharacterInfoController invoker, CharacterInfoController target);
    IEnumerator Cooldown(float time);
}

[Serializable]
public struct AbilityInfo
{
    public string ClassName;
    public string Name;
    public string Description;
    public float Cooldown;
    public bool Avaliable;
    public int BonusPower;
}
