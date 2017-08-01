using System.Collections;
using Game.Character;
using UnityEngine;

public class HealAbility : IAbility
{
    private readonly int minHeal = 20;
    private readonly int maxHeal = 35;

    public string ClassName { get; private set; }

    public AbilityInfo AbilityInfo
    {
        get
        {
            return abilityInfo;
        }
    }

    private AbilityInfo abilityInfo;
    
    public HealAbility(AbilityInfo info)
    {
        abilityInfo = info;
        ClassName = GetType().FullName;
    }

    public void Invoke(CharacterInfoController invoker, CharacterInfoController target)
    {
        int healValue = Random.Range(minHeal, maxHeal + 1);
        
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
