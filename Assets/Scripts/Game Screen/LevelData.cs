using UnityEngine;

[CreateAssetMenu(menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    public Game.Character.CharacterInfo Player;
    public Game.Character.CharacterInfo[] Allies;
    public Game.Character.CharacterInfo[] Enemies;
}
