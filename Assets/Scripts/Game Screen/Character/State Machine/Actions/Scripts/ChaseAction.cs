using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action 
{
	public override void Act (StateController controller)
	{
		Chase (controller); 
	}

	private void Chase(StateController controller)
	{
		if (Vector3.Distance(controller.transform.position, controller.ChaseTarget.position) >
		    controller.NavMeshAgent.stoppingDistance)
		{
			controller.NavMeshAgent.isStopped = false;
			controller.NavMeshAgent.destination = controller.ChaseTarget.position;
		}
		else
		{
			controller.NavMeshAgent.isStopped = true;
		}
	}
}
