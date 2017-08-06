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
        set { healInfo = value; }
    }

    public override AbilityInvokeErrorCode Invoke(CharacterInfoController invoker, CharacterInfoController target)
    {
        int healValue = Random.Range(healInfo.MinHeal, HealInfo.MaxHeal + 1);
        
        if (target == null || target.StateInfo.CurrentState == CharacterState.StateName.DEAD)
        {
            invoker.Heal(healValue);
        }
        else
        {
            if (invoker.Info.AllyTags.Contains(target.Info.Tag))
            {
                if (Vector3.Distance(invoker.transform.position, target.transform.position) -
                    (invoker.Size + target.Size) <= AbilityInfo.CastDistance)
                {
                    target.Heal(healValue);
                }
                else
                {
                    return AbilityInvokeErrorCode.TOO_FAR;
                }
            }
            else
            {
                invoker.Heal(healValue);
            }
        }

        return AbilityInvokeErrorCode.NO_ERROR;
    }
}
