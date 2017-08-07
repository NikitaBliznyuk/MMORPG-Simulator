using Game.Character;
using UnityEngine;

public class Tank : MonoBehaviour, IInputController, IAllyBaseStates
{
	public CharacterInfoController CurrentObservableInfo { get; set; }
	
	[Header("Abilities indices")]
    
	[SerializeField] private int simpleAttack;
	
	private delegate void StateHandler();
	private StateHandler  CurrentState;

	private CharacterMovementController movementController;

	private void Awake()
	{
		CurrentState = Move;
	}

	private void Start()
	{
		movementController = GetComponent<CharacterMovementController>();
	}

	private void Update()
	{
		CurrentState();
	}

	public void Idle()
	{
		// TODO THINK A LITTLE OF THIS STATE
	}

	private int currentPathIndex;
	public void Move()
	{
		Vector3 nextPosition = LevelPath.Instance.NextPosition(currentPathIndex);
		movementController.MoveToPoint(nextPosition);

		if (Vector3.Distance(nextPosition, transform.position) < 0.1f && currentPathIndex < LevelPath.Instance.PathCount)
		{
			currentPathIndex++;
		}
	}

	public void Attack()
	{
		
	}
}
