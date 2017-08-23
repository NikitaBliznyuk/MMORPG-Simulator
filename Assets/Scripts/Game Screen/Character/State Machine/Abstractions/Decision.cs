using UnityEngine;

namespace GameScreen.Character.StateMachine
{
	public abstract class Decision : ScriptableObject
	{
		public abstract bool Decide(StateController controller);
	}
}