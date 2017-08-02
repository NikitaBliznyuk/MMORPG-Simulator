using Game.Character;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Heal ability")]
public class HealAbility : Ability, IHeal
{
    [SerializeField] private HealInfo healInfo;

    public HealInfo HealInfo
    {
        get { return healInfo; }
    }

    public override bool Invoke(CharacterInfoController invoker, CharacterInfoController target)
    {
        int healValue = Random.Range(healInfo.MinHeal, HealInfo.MaxHeal + 1);
        
        if (target == null)
        {
            invoker.Heal(healValue);
        }
        else
        {
            if (target.Info.AllyTag == invoker.Info.AllyTag)
            {
                target.Heal(healValue);
            }
            else
            {
                invoker.Heal(healValue);
            }
        }

        invoker.StartCoroutine(Cooldown(AbilityInfo.Cooldown));

        return true;
    }
}
