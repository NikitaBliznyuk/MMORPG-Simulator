using System;
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

	[Header("Abilities")] 
	
	[SerializeField] private int[] simpleAttacksIndices;

	private int nextWayPoint;

	public CharacterInfoController InfoController { get; private set; }
	public float AggroRange { get { return aggroRange; } }
	public Transform ChaseTarget { get; set; }
	public NavMeshAgent NavMeshAgent { get; private set; }

	#region Abilities getters

	public int[] SimpleAttacksIndices
	{
		get { return simpleAttacksIndices; }
	}

	#endregion
	
	private void Awake () 
	{
		NavMeshAgent = GetComponent<NavMeshAgent> ();
		InfoController = GetComponent<CharacterInfoController>();
		
		InfoController.StateInfo.ChangeState += StateInfoOnChangeState;
	}

	private void StateInfoOnChangeState()
	{
		enabled = InfoController.StateInfo.CurrentState != CharacterState.StateName.DEAD;
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