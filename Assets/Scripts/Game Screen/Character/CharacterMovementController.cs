using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

public class CharacterMovementController : MonoBehaviour
{
    private IInputController inputController;
    private CharacterInfo info;

    private void Awake()
    {
        inputController = GetComponent<IInputController>();
        info = GetComponent<CharacterInfo>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, inputController.NextPosition,
            info.MovementSpeed * Time.deltaTime);
    }
}
