using Game.Character;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterInfoController))]
public class StateController : MonoBehaviour
{
	[Header("References")]
	[SerializeField]
	[Tooltip("Current character state.")]
	private State currentState;

	[SerializeField]
	[Tooltip("Remain state.")]
	private State remainState;

	[Header("Settings")]
	[SerializeField]
	[Tooltip("Positions in World Space. Character will move from one to another.")]
	private Transform[] wayPointList;

	[SerializeField]
	[Tooltip("Range of aggro in World Space.")]
	private float aggroRange;

	[Header("Abilities")]
	[SerializeField]
	[Tooltip("Attack indices in abilities array.")]
	private int[] simpleAttacksIndices;

	/// <summary>
	/// Next way point in way point list.
	/// </summary>
	private int nextWayPoint;

	/// <summary>
	/// Reference to CharacterInfoController instance. It contains all character info (abilities, stats, etc.).
	/// </summary>
	public CharacterInfoController InfoController { get; private set; }

	/// <summary>
	/// Range of aggro.
	/// </summary>
	public float AggroRange
	{
		get { return aggroRange; }
	}

	/// <summary>
	/// Reference to chasing target.
	/// </summary>
	public Transform ChaseTarget { get; set; }

	/// <summary>
	/// Reference to NavMesh Agent.
	/// </summary>
	public NavMeshAgent NavMeshAgent { get; private set; }

	#region Abilities getters

	/// <summary>
	/// Get indices of simple attacks from abilities array.
	/// </summary>
	public int[] SimpleAttacksIndices
	{
		get { return simpleAttacksIndices; }
	}

	#endregion

	private void Awake()
	{
		NavMeshAgent = GetComponent<NavMeshAgent>();
		InfoController = GetComponent<CharacterInfoController>();

		InfoController.StateInfo.ChangeState += StateInfoOnChangeState;
	}

	/// <summary>
	/// Executes, when character change internal state.
	/// </summary>
	private void StateInfoOnChangeState()
	{
		enabled = InfoController.StateInfo.CurrentState != CharacterState.StateName.DEAD;
	}

	private void Update()
	{
		currentState.UpdateState(this);
	}

	/// <summary>
	/// Transition to derived state.
	/// </summary>
	/// <param name="nextState">Next state</param>
	public void TransitionToState(State nextState)
	{
		if (nextState != remainState)
		{
			currentState = nextState;
			OnExitState();
		}
	}

	/// <summary>
	/// Executes after changing state.
	/// </summary>
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