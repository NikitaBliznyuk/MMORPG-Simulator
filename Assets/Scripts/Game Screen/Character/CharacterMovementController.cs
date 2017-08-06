using Game.Character;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    private IInputController inputController;
    private CharacterInfoController infoController;

    private void Start()
    {
        inputController = GetComponent<IInputController>();
        infoController = GetComponent<CharacterInfoController>();
        
        infoController.StateInfo.ChangeState += OnChangeState;
    }

    private void OnChangeState()
    {
        if (infoController.StateInfo.CurrentState == CharacterState.StateName.DEAD)
            enabled = false;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, inputController.NextPosition,
            infoController.Info.MovementSpeed * Time.deltaTime);
    }
}
