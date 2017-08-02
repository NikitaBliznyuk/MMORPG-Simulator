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
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, inputController.NextPosition,
            infoController.Info.MovementSpeed * Time.deltaTime);
    }
}
