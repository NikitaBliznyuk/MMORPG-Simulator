using GameScreen.Character.Abilities;
using UnityEngine;

namespace GameScreen.Character.StateMachine
{
	[CreateAssetMenu(menuName = "PluggableAI/Actions/Simple attack")]
	public class SimpleAttackAction : Action
	{
		public override void Act(StateController controller)
		{
			Attack(controller);
		}

		private void Attack(StateController controller)
		{
			foreach (var attacksIndex in controller.SimpleAttacksIndices)
			{
				AbilityInvokeErrorCode code = controller.InfoController.InvokeAbility(attacksIndex,
					controller.ChaseTarget.GetComponent<CharacterInfoController>());

				if (code == AbilityInvokeErrorCode.NO_ERROR)
				{
					break;
				}
			}
		}
	}
}
