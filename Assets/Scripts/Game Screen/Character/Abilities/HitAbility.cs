using Game.Character;
using UnityEngine;

public class HitAbility : IAbility
{
	private readonly int minDamage = 10;
	private readonly int maxDamage = 15;
	
	public void Invoke(CharacterInfoController invoker, CharacterInfoController target)
	{
		if (target != null && target.EnemyTag == invoker.AllyTag)
		{
			target.DealDamage(Random.Range(minDamage, maxDamage + 1));
		}
	}
}
