using UnityEngine;

namespace GameScreen.Character.StateMachine
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Walk")]
    public class WalkAction : Action
    {
        public override void Act(StateController controller)
        {
            Walk(controller);
        }

        private void Walk(StateController controller)
        {
            if (controller.NavMeshAgent.remainingDistance < controller.NavMeshAgent.stoppingDistance)
            {
                controller.NextWayPoint++;
            }
        }
    }
}
