using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

[CreateAssetMenu(menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private CharacterInfo player;
    [SerializeField] private CharacterInfo[] allies;
    [SerializeField] private CharacterInfo[] enemies;

    public CharacterInfo Player
    {
        get { return player; }
    }

    public CharacterInfo[] Allies
    {
        get { return allies; }
    }

    public CharacterInfo[] Enemies
    {
        get { return enemies; }
    }
}
