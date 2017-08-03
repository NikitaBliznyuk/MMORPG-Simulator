using System.Linq;
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
            if (invoker.Info.AllyTags.Contains(target.Info.Tag))
            {
                if (Vector3.Distance(invoker.transform.position, target.transform.position) <= AbilityInfo.CastDistance)
                {
                    target.Heal(healValue);
                }
                else
                {
                    return false;
                }
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
