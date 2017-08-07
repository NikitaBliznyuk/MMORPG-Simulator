using Game.Character;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField] private CharacterInfoController characterPrefab;
    [SerializeField] private RangeVisualizer rangeVisualizerPrefab;

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
        
        LevelCurrentData levelData = new LevelCurrentData
        {
            PlayerReference = player
        };

        foreach (var ally in data.Allies)
        {
            CreateAlly(ally);
        }
        
        UpdateData(levelData);
    }

    private CharacterInfoController CreatePlayer(SpawnData data)
    {
        CharacterInfoController player = Instantiate(characterPrefab);
        player.transform.position = Vector3.zero; // TODO MAKE ENTER POINT IN DUNGEON
        player.gameObject.AddComponent<ClickController>();
        player.Info = data.CharacterInfo;
        player.Icon = data.Icon;
        player.tag = data.CharacterInfo.Tag; // Unnecessary. Just to see in inspector.

        RangeVisualizer rangeVisualizer = Instantiate(rangeVisualizerPrefab, player.transform);
        rangeVisualizer.name = rangeVisualizerPrefab.name; // Unnecessary. Just for beauty. :)
        player.RangeVisualizer = rangeVisualizer;
        
        return player;
    }

    private CharacterInfoController CreateAlly(SpawnData data)
    {
        CharacterInfoController ally = Instantiate(characterPrefab);
        ally.transform.position = Vector3.zero; // TODO MAKE ENTER POINT IN DUNGEON
        ally.gameObject.AddComponent<Tank>();
        ally.Info = data.CharacterInfo;
        ally.Icon = data.Icon;
        ally.tag = data.CharacterInfo.Tag; // Unnecessary.

        return ally;
    }
}

public class LevelCurrentData
{
    public CharacterInfoController PlayerReference;
}
