using System.Linq;
using Game.Character;
using UnityEngine;

public class BotStateMachine : MonoBehaviour, IInputController, IBossBaseStates
{
    private CharacterInfoController characterInfoController;
    private CharacterMovementController movementController;
    
    public CharacterInfoController CurrentObservableInfo { get; set; }
    
    [Header("Settings")]
    
    [SerializeField] private float aggroRange;
    
    [Header("Abilities indices")]
    
    [SerializeField] private int simpleAttack;

    private delegate void StateHandler();
    private StateHandler CurrentState;

    private CharacterInfoController[] enemies;

    private void Awake()
    {
        Loader.DataUpdated += OnDataUpdated;

        CurrentState = Idle;
    }

    private void Start()
    {
        characterInfoController = GetComponent<CharacterInfoController>();
        characterInfoController.StateInfo.ChangeState += OnChangeState;

        foreach (var ability in characterInfoController.Info.Abilities)
        {
            ability.Avaliable = true;
        }

        movementController = GetComponent<CharacterMovementController>();
    }

    private void OnDataUpdated(LevelCurrentData data)
    {
        enemies = FindObjectsOfType<CharacterInfoController>()
            .Where(controller => controller.Info.EnemyTags.Contains(characterInfoController.Info.Tag))
            .ToArray();
    }

    private void OnChangeState()
    {
        if(characterInfoController.StateInfo.CurrentState == CharacterState.StateName.DEAD)
            enabled = false;
    }

    private void Update()
    {
        CurrentState();
    }

    #region States

    public void Idle()
    {
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) <= aggroRange)
                {
                    CurrentObservableInfo = enemy;
                    CurrentState = Attack;
                    break;
                }
            }
        }
    }

    public void Attack()
    {
        AbilityInvokeErrorCode code = characterInfoController.InvokeAbility(simpleAttack, CurrentObservableInfo);

        Vector3 nextPosition = code == AbilityInvokeErrorCode.TOO_FAR
            ? CurrentObservableInfo.transform.position
            : transform.position;
        
        movementController.MoveToPoint(nextPosition);
    }

    #endregion
}
