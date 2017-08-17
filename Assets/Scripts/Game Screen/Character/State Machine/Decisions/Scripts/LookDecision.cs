using System.Linq;
using Game.Character;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision 
{
	public override bool Decide(StateController controller)
	{
		bool enemyInRange = EnemyInRange(controller);
		return enemyInRange;
	}

	private bool EnemyInRange(StateController controller)
	{
		Collider[] colliders = Physics.OverlapSphere(controller.transform.position, controller.AggroRange,
			LayerMask.GetMask("Clickable"));

		if (colliders == null)
			return false;

		CharacterInfoController[] enemies = colliders
			.Where(collider => collider.GetComponentInParent<CharacterInfoController>() != null &&
			                   collider.GetComponentInParent<CharacterInfoController>().Info.EnemyTags
				                   .Contains(controller.InfoController.Info.Tag))
			.OrderBy(collider => Vector3.Distance(controller.transform.position, collider.transform.position))
			.Select(collider => collider.GetComponentInParent<CharacterInfoController>()).ToArray();

		CharacterInfoController enemy = enemies.Length > 0 ? enemies[0] : null;

		if (enemy == null)
		{
			return false;
		}

		controller.ChaseTarget = enemy.transform;
		return true;
	}
}
