using System.Linq;
using Game.Character;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Hit ability")]
public class HitAbility : Ability, IHit
{
	[SerializeField] private HitInfo hitInfo;

	public HitInfo HitInfo
	{
		get { return hitInfo; }
		set { hitInfo = value; }
	}

	public override AbilityInvokeErrorCode Invoke(CharacterInfoController invoker, CharacterInfoController target)
	{
		if (target == null)
			return AbilityInvokeErrorCode.WRONG_TARGET;

		if (!invoker.Info.EnemyTags.Contains(target.Info.Tag))
			return AbilityInvokeErrorCode.WRONG_TARGET;

		if (Vector3.Distance(invoker.transform.position, target.transform.position) - (invoker.Size + target.Size) >
		    AbilityInfo.CastDistance)
			return AbilityInvokeErrorCode.TOO_FAR;
		
		int hitValue = Random.Range(hitInfo.MinDamage, hitInfo.MaxDamage + 1);
		target.DealDamage(hitValue);

		invoker.StartCoroutine(Cooldown(AbilityInfo.Cooldown));

		return AbilityInvokeErrorCode.NO_ERROR;
	}
}
