using System;
using System.Collections;
using Game.Character;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] private AbilityInfo abilityInfo;
    
    private bool avaliable = true;

    public AbilityInfo AbilityInfo
    {
        get { return abilityInfo; }
        set { abilityInfo = value; }
    }

    public bool Avaliable
    {
        get { return avaliable; }
        set { avaliable = value; }
    }

    public abstract AbilityInvokeErrorCode Invoke(CharacterInfoController invoker, CharacterInfoController target);
    
    public IEnumerator Cooldown(float time)
    {
        avaliable = false;
        yield return new WaitForSeconds(time);
        avaliable = true;
    }
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
    NO_ERROR, TOO_FAR, NO_ENERGY, NO_SUCH_ABILITY, NOT_AVALIABLE, WRONG_TARGET
}
