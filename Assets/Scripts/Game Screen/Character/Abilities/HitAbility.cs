using System.Collections;
using Game.Character;
using UnityEngine;

public class HitAbility : IAbility
{
	private readonly int minDamage = 10;
	private readonly int maxDamage = 15;

	public string ClassName { get; private set; }

	public AbilityInfo AbilityInfo
	{
		get
		{
			return abilityInfo;
		}
	}

	private AbilityInfo abilityInfo;
    
	public HitAbility(AbilityInfo info)
	{
		abilityInfo = info;
		ClassName = GetType().FullName;
	}

	public void Invoke(CharacterInfoController invoker, CharacterInfoController target)
	{
		if (target != null && target.EnemyTag == invoker.AllyTag)
		{
			target.DealDamage(Random.Range(minDamage, maxDamage + 1));
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
