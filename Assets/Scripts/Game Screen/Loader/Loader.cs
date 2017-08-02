using Game.Character;
using UnityEngine;

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
        
        LevelCurrentData levelData = new LevelCurrentData
        {
            PlayerReference = player
        };
        UpdateData(levelData);
    }

    private CharacterInfoController CreatePlayer(Game.Character.CharacterInfo info)
    {
        CharacterInfoController player = Instantiate(characterPrefab);
        player.Info = info;
        return player;
    }
}

public class LevelCurrentData
{
    public CharacterInfoController PlayerReference;
}
