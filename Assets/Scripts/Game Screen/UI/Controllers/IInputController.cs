using Game.Character;
using UnityEngine;

public interface IInputController
{
    CharacterInfoController CurrentObservableInfo { get; }
    Vector3 NextPosition { get; }
}
