using Game.Character;
using UnityEngine;

public interface IInputController
{
    CharacterInfoController CurrentObservableInfo { get; set; }
    Vector3 NextPosition { get; }
}
