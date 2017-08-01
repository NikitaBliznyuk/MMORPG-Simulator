using System.Collections;
using Game.Character;
using UnityEngine;

public class HitAbility : IAbility, IHit
{
	public AbilityInfo abilityInfo;
	public HitInfo hitInfo;

	public HitAbility(AbilityInfo abilityInfo, HitInfo hitInfo)
	{
		this.abilityInfo = abilityInfo;
		this.hitInfo = hitInfo;
		ClassName = GetType().FullName;
	}

	public string ClassName { get; private set; }

	public AbilityInfo AbilityInfo
	{
		get { return abilityInfo; }
	}

	public HitInfo HitInfo
	{
		get { return hitInfo; }
	}

	public void Invoke(CharacterInfoController invoker, CharacterInfoController target)
	{
		if (target != null && target.EnemyTag == invoker.AllyTag)
		{
			target.DealDamage(Random.Range(hitInfo.MinDamage, HitInfo.MaxDamage + 1));
			invoker.StartCoroutine(Cooldown(abilityInfo.Cooldown));
		}
	}
	
	public IEnumerator Cooldown(float time)
	{
		abilityInfo.Avaliable = false;
		yield return new WaitForSeconds(time);
		abilityInfo.Avaliable = true;
	}
}
