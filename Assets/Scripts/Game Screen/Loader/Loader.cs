using System.Linq;
using Game.Character;
using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

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
        UpdateData(levelData);
    }

    private CharacterInfoController CreatePlayer(SpawnData info)
    {
        CharacterInfoController player = Instantiate(characterPrefab);
        player.transform.position = Vector3.zero; // TODO MAKE ENTER POINT IN DUNGEON
        player.gameObject.AddComponent<ClickController>();
        player.Info = info.CharacterInfo;
        player.Icon = info.Icon;
        player.tag = info.CharacterInfo.Tag; // Unnecessary. Just to see in inspector.

        RangeVisualizer rangeVisualizer = Instantiate(rangeVisualizerPrefab, player.transform);
        rangeVisualizer.name = rangeVisualizerPrefab.name; // Unnecessary. Just for beauty. :)
        player.RangeVisualizer = rangeVisualizer;
        
        return player;
    }
}

public class LevelCurrentData
{
    public CharacterInfoController PlayerReference;
}
