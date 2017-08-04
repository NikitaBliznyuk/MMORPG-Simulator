using System;
using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

[CreateAssetMenu(menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private SpawnData player;
    [SerializeField] private SpawnData[] allies;
    [SerializeField] private SpawnData[] enemies;

    public SpawnData Player
    {
        get { return player; }
    }

    public SpawnData[] Allies
    {
        get { return allies; }
    }

    public SpawnData[] Enemies
    {
        get { return enemies; }
    }
}

[Serializable]
public struct SpawnData
{
    public CharacterInfo CharacterInfo;
    public Sprite Icon;
    public Vector2 SpawnPosition;
}
