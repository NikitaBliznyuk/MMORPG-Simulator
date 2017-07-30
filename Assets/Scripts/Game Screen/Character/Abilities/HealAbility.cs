using System.Collections;
using Game.Character;
using UnityEngine;

public class HealAbility : IAbility
{
    private readonly int minHeal = 15;
    private readonly int maxHeal = 25;

    public AbilityInfo AbilityInfo
    {
        get
        {
            return abilityInfo;
        }
    }

    private AbilityInfo abilityInfo;
    
    public HealAbility()
    {
        abilityInfo.Name = "Heal";
        abilityInfo.Description = "Heal one target on 15-25 health.";
        abilityInfo.Cooldown = 1.5f;
        abilityInfo.Avaliable = true;
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
