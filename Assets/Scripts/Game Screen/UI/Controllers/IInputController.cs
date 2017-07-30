using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

public interface IInputController
{
    CharacterInfo CurrentObservableInfo { get; }
    Vector3 NextPosition { get; }
}
