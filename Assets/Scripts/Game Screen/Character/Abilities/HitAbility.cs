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
	}

	public override bool Invoke(CharacterInfoController invoker, CharacterInfoController target)
	{
		if(target == null)
			return false;
		
		if(!invoker.Info.EnemyTags.Contains(target.Info.Tag))
			return false;
		Debug.Log(invoker.Info.StatsInfo.Name);

		if (Vector3.Distance(invoker.transform.position, target.transform.position) > AbilityInfo.CastDistance)
			return false;
		
		int hitValue = Random.Range(hitInfo.MinDamage, hitInfo.MaxDamage + 1);
		target.DealDamage(hitValue);

		invoker.StartCoroutine(Cooldown(AbilityInfo.Cooldown));

		return true;
	}
}
