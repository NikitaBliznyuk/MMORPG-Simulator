using Game.Character;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterInfoController))]
public class StateController : MonoBehaviour
{
	private enum WayType
	{
		Patrol,
		Walking
	}

	[Header("References")]
	[SerializeField]
	[Tooltip("Current character state.")]
	private State currentState;

	[SerializeField]
	[Tooltip("Remain state.")]
	private State remainState;

	[Header("Settings")]
	[SerializeField]
	[Tooltip("Range of aggro in World Space.")]
	private float aggroRange;

	[SerializeField]
	[Tooltip("Patrol way points (if needed).")]
	private Vector3[] wayPointList;

	[SerializeField]
	[Tooltip("Way, that used by characted to walk through. Walking means walk through level, patrol - patrol around.")]
	private WayType wayType;

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

	/// <summary>
	/// Reference to next way point. Set next destination to NavMesh agent.
	/// </summary>
	public int NextWayPoint
	{
		get { return nextWayPoint; }
		set
		{
			nextWayPoint = wayType == WayType.Patrol
				? value % WayPointList.Length
				: Mathf.Min(value, WayPointList.Length - 1);
			NavMeshAgent.destination = WayPointList[nextWayPoint];
		}
	}

	/// <summary>
	/// Way in the level. Every level has it's own path.
	/// </summary>
	private Vector3[] WayPointList
	{
		get
		{
			return wayType == WayType.Walking
				? LevelPath.Instance.Path
				: wayPointList.Length > 0
					? wayPointList
					: new[] {transform.position};
		}
	}

	#region Abilities getters

	/// <summary>
	/// Get indices of simple attacks from abilities array.
	/// </summary>
	public int[] SimpleAttacksIndices
	{
		get { return simpleAttacksIndices; }
	}

	#endregion

	#region Unity functions

	private void Awake()
	{
		NavMeshAgent = GetComponent<NavMeshAgent>();
		InfoController = GetComponent<CharacterInfoController>();

		InfoController.StateInfo.ChangeState += StateInfoOnChangeState;
	}

	private void Update()
	{
		currentState.UpdateState(this);
	}

	#endregion

	/// <summary>
	/// Executes, when character change internal state.
	/// </summary>
	private void StateInfoOnChangeState()
	{
		enabled = InfoController.StateInfo.CurrentState != CharacterState.StateName.DEAD;
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

		Gizmos.color = Color.cyan;
		foreach (var point in wayPointList)
		{
			Gizmos.DrawWireSphere(point, 0.5f);
		}
	}

	#endregion
}