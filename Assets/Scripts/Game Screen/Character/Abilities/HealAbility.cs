using Game.Character;
using UnityEngine;

public class HealAbility : IAbility
{
    private readonly int minHeal = 15;
    private readonly int maxHeal = 25;
    
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
    }
}
