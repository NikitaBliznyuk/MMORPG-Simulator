using Game.Character;
using UnityEngine;

public class Tank : MonoBehaviour, IInputController, IAllyBaseStates
{
	public CharacterInfoController CurrentObservableInfo { get; set; }
	public Vector3 NextPosition { get; private set; }
	
	[Header("Abilities indices")]
    
	[SerializeField] private int simpleAttack;
	
	private delegate void StateHandler();
	private StateHandler  CurrentState;

	private void Awake()
	{
		CurrentState = Move;
	}

	private void Update()
	{
		CurrentState();
	}

	public void Idle()
	{
		// TODO THINK A LITTLE OF THIS STATE
	}

	public void Move()
	{
		NextPosition = LevelPath.Instance.NextPosition(transform.position);
	}

	public void Attack()
	{
		
	}
}
