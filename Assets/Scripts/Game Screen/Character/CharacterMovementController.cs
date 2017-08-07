using Game.Character;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovementController : MonoBehaviour
{
    private CharacterInfoController infoController;
    private NavMeshAgent navMeshAgent;
    
    private void Start()
    {
        infoController = GetComponent<CharacterInfoController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.speed = infoController.Info.MovementSpeed;
        
        infoController.StateInfo.ChangeState += OnChangeState;
    }

    private void OnChangeState()
    {
        if (infoController.StateInfo.CurrentState == CharacterState.StateName.DEAD)
        {
            navMeshAgent.enabled = false;
            enabled = false;
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
