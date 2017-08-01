using System.Collections;
using Game.Character;
using UnityEngine;

public class HealAbility : IAbility, IHeal
{
    public AbilityInfo abilityInfo;
    public HealInfo healInfo;

    public HealAbility(AbilityInfo abilityInfo, HealInfo healInfo)
    {
        this.abilityInfo = abilityInfo;
        this.healInfo = healInfo;
        ClassName = GetType().FullName;
    }

    public string ClassName { get; private set; }

    public AbilityInfo AbilityInfo
    {
        get { return abilityInfo; }
    }

    public HealInfo HealInfo
    {
        get { return healInfo; }
    }

    public void Invoke(CharacterInfoController invoker, CharacterInfoController target)
    {
        int healValue = Random.Range(healInfo.MinHeal, HealInfo.MaxHeal + 1);
        
        if (target == null)
        {
            invoker.Heal(healValue);
        }
        else
        {
            if (target.AllyTag == invoker.AllyTag)
            {
                target.Heal(healValue);
            }
            else
            {
                invoker.Heal(healValue);
            }
        }

        invoker.StartCoroutine(Cooldown(abilityInfo.Cooldown));
    }

    public IEnumerator Cooldown(float time)
    {
        abilityInfo.Avaliable = false;
        yield return new WaitForSeconds(time);
        abilityInfo.Avaliable = true;
    }
}
