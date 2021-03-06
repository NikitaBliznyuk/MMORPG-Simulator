﻿using System.Linq;
using UnityEngine;

namespace GameScreen.Character.Abilities
{
	[CreateAssetMenu(menuName = "Abilities/Hit ability")]
	public class HitAbility : Ability, IHit
	{
		[SerializeField]
		private HitInfo hitInfo;

		public HitInfo HitInfo
		{
			get { return hitInfo; }
			set { hitInfo = value; }
		}

		public override AbilityInvokeErrorCode Invoke(CharacterInfoController invoker, CharacterInfoController target)
		{
			if (target == null || target.StateInfo.CurrentState == CharacterState.StateName.DEAD)
				return AbilityInvokeErrorCode.WRONG_TARGET;

			if (!invoker.Info.EnemyTags.Contains(target.Info.Tag))
				return AbilityInvokeErrorCode.WRONG_TARGET;

			if (Vector3.Distance(invoker.transform.position, target.transform.position) - (invoker.Size + target.Size) >
			    AbilityInfo.CastDistance)
				return AbilityInvokeErrorCode.TOO_FAR;

			int hitValue = Random.Range(hitInfo.MinDamage, hitInfo.MaxDamage + 1);
			target.DealDamage(hitValue);

			return AbilityInvokeErrorCode.NO_ERROR;
		}
	}
}
