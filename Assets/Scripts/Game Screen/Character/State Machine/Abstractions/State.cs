using UnityEngine;

namespace GameScreen.Character.StateMachine
{
	[CreateAssetMenu(menuName = "PluggableAI/State")]
	public class State : ScriptableObject
	{
		[SerializeField]
		private Action[] actions;

		[SerializeField]
		private Transition[] transitions;

		[SerializeField]
		private Color currentStateGizmoColor = Color.gray;

		public Color CurrentStateGizmoColor
		{
			get { return currentStateGizmoColor; }
		}

		public void UpdateState(StateController controller)
		{
			DoActions(controller);
			CheckTransitions(controller);
		}

		private void DoActions(StateController controller)
		{
			foreach (Action action in actions)
			{
				action.Act(controller);
			}
		}

		private void CheckTransitions(StateController controller)
		{
			foreach (Transition transition in transitions)
			{
				bool decisionSucceeded = transition.decision.Decide(controller);

				controller.TransitionToState(decisionSucceeded ? transition.trueState : transition.falseState);
			}
		}
	}
}