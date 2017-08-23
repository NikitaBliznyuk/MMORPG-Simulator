using System;
using System.Collections;
using Game.Character;
using UnityEngine;

/// <summary>
/// Generic class for all abilities.
/// </summary>
public abstract class Ability : ScriptableObject
{
    [SerializeField]
    private AbilityInfo abilityInfo;

    public AbilityInfo AbilityInfo
    {
        get { return abilityInfo; }
        set { abilityInfo = value; }
    }

    public abstract AbilityInvokeErrorCode Invoke(CharacterInfoController invoker, CharacterInfoController target);
}

[Serializable]
public struct AbilityInfo
{
    public string Name;
    public string Description;
    public float Cooldown;
    public int Cost;
    public float CastDistance;
}

public enum AbilityInvokeErrorCode
{
    NO_ERROR,
    TOO_FAR,
    NO_ENERGY,
    NO_SUCH_ABILITY,
    NOT_AVALIABLE,
    WRONG_TARGET
}