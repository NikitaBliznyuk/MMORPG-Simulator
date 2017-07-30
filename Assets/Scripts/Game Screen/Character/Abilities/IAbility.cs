using Game.Character;

public interface IAbility
{
    void Invoke(CharacterInfoController invoker, CharacterInfoController target);
}
