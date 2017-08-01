using System;
using System.Collections;
using Game.Character;

public interface IAbility
{
    string ClassName { get; }
    AbilityInfo AbilityInfo { get; }
    void Invoke(CharacterInfoController invoker, CharacterInfoController target);
    IEnumerator Cooldown(float time);
}

[Serializable]
public struct AbilityInfo
{
    public string Name;
    public string Description;
    public float Cooldown;
    public int Cost;
    public bool Avaliable;
}
