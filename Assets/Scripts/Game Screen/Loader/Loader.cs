using System.Linq;
using Game.Character;
using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

public class Loader : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField] private CharacterInfoController characterPrefab;

    public delegate void LevelStartHandler(LevelData data);
    public static event LevelStartHandler LevelStart;
    public delegate void LevelEndHandler();
    public static event LevelEndHandler LevelEnd;
    public delegate void DataUpdatedHandler(LevelCurrentData data);
    public static event DataUpdatedHandler DataUpdated;

    private void Awake()
    {
        LevelStart += Initialize;
    }

    public static void StartLevel(LevelData data)
    {
        if (LevelStart != null)
            LevelStart(data);
    }

    public static void UpdateData(LevelCurrentData data)
    {
        if (DataUpdated != null)
            DataUpdated(data);
    }

    private void Initialize(LevelData data)
    {
        CharacterInfoController player = CreatePlayer(data.Player);
        CreateEnemies(data.Enemies);
        
        LevelCurrentData levelData = new LevelCurrentData
        {
            PlayerReference = player
        };
        UpdateData(levelData);
    }

    private CharacterInfoController CreatePlayer(SpawnData info)
    {
        CharacterInfoController player = Instantiate(characterPrefab);
        player.transform.position = info.SpawnPosition;
        player.gameObject.AddComponent<ClickController>();
        player.Info = info.CharacterInfo;
        player.tag = info.CharacterInfo.Tag; // Unnecessary. Just to see in inspector.
        return player;
    }

    private void CreateEnemies(SpawnData[] infos)
    {
        foreach (var info in infos)
        {
            CharacterInfoController enemy = Instantiate(characterPrefab);
            enemy.transform.position = info.SpawnPosition;
            enemy.gameObject.AddComponent<BotInputController>();
            enemy.Info = info.CharacterInfo;
            enemy.tag = info.CharacterInfo.Tag; // Unnecessary. Just to see in inspector.
        }
    }
}

public class LevelCurrentData
{
    public CharacterInfoController PlayerReference;
}
