﻿using Game.Character;
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
		
		if(target.Info.EnemyTag == invoker.Info.EnemyTag)
			return false;
		
		Debug.Log("Distance: " + Vector3.Distance(invoker.transform.position, target.transform.position));

		if (Vector3.Distance(invoker.transform.position, target.transform.position) > AbilityInfo.CastDistance)
			return false;
		
		int hitValue = Random.Range(hitInfo.MinDamage, hitInfo.MaxDamage + 1);
		target.DealDamage(hitValue);

		invoker.StartCoroutine(Cooldown(AbilityInfo.Cooldown));

		return true;
	}
}
