using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateController : MonoBehaviour {

	[SerializeField] private State currentState;
	[SerializeField] private State remainState;
	
	[SerializeField] private List<Transform> wayPointList;

	private NavMeshAgent navMeshAgent;
	private int nextWayPoint;
	private Transform chaseTarget;

	private void Awake () 
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
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
}