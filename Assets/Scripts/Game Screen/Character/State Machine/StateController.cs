using System.Collections.Generic;
using Game.Character;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterInfoController))]
public class StateController : MonoBehaviour {

	[Header("References")]
	
	[SerializeField] private State currentState;
	[SerializeField] private State remainState;
	
	[Header("Settings")]
	
	[SerializeField] private Transform[] wayPointList;
	[SerializeField] private float aggroRange;

	private int nextWayPoint;

	public CharacterInfoController InfoController { get; private set; }
	public float AggroRange { get { return aggroRange; } }
	public Transform ChaseTarget { get; set; }
	public NavMeshAgent NavMeshAgent { get; private set; }

	private void Awake () 
	{
		NavMeshAgent = GetComponent<NavMeshAgent> ();
		InfoController = GetComponent<CharacterInfoController>();
	}
	private void Update()
	{
		currentState.UpdateState (this);
	}

	public void TransitionToState(State nextState)
	{
		if (nextState != remainState) 
		{
			currentState = nextState;
			OnExitState ();
		}
	}

	private void OnExitState()
	{
		
	}

	#region Editor only

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = currentState.CurrentStateGizmoColor;
		Gizmos.DrawWireSphere(transform.position, aggroRange);
	}

	#endregion
}