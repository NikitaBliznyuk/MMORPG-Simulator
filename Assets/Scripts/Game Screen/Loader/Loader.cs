using GameScreen.Character;
using GameScreen.Level;
using UnityEngine;

namespace GameScreen.Loader
{
    /// <summary>
    /// Raise start level and end level events. Creates instances of player and his allies.
    /// </summary>
    public class Loader : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        [Tooltip("Character prefab, that contains player info.")]
        private CharacterInfoController playerPrefab;

        [SerializeField]
        [Tooltip("Character prefab, that contains ally info.")]
        private CharacterInfoController allyPrefab;

        /// <summary>
        /// Delegate for level start event.
        /// </summary>
        /// <param name="data">Data that need to be loaded.</param>
        public delegate void LevelStartHandler(LevelData data);

        /// <summary>
        /// Level start event.
        /// </summary>
        public static event LevelStartHandler LevelStart;

        /// <summary>
        /// Delegate for level ending.
        /// </summary>
        public delegate void LevelEndHandler();

        //public static event LevelEndHandler LevelEnd;

        /// <summary>
        /// Delegate for data update event.
        /// </summary>
        /// <param name="data">Current level data.</param>
        public delegate void DataUpdatedHandler(LevelCurrentData data);

        /// <summary>
        /// Data update event.
        /// </summary>
        public static event DataUpdatedHandler DataUpdated;

        private void Awake()
        {
            LevelStart += Initialize;
        }

        /// <summary>
        /// Static function, that invoke LevelStart event.
        /// </summary>
        /// <param name="data">Data that need to be loaded.</param>
        public static void StartLevel(LevelData data)
        {
            if (LevelStart != null)
                LevelStart(data);
        }

        /// <summary>
        /// Static function, that invoke DateUpdated event.
        /// </summary>
        /// <param name="data">Data that needed to be updated.</param>
        public static void UpdateData(LevelCurrentData data)
        {
            if (DataUpdated != null)
                DataUpdated(data);
        }

        /// <summary>
        /// Level initialization.
        /// </summary>
        /// <param name="data">Initialization data.</param>
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

        /// <summary>
        /// Get player reference.
        /// </summary>
        /// <param name="data">Player data.</param>
        /// <returns>Reference to instantiated player.</returns>
        private CharacterInfoController CreatePlayer(SpawnData data)
        {
            CharacterInfoController player = Instantiate(playerPrefab);

            player.transform.position = Vector3.zero; // TODO MAKE ENTER POINT IN DUNGEON
            player.gameObject.AddComponent<ClickController>();
            player.Info = data.CharacterInfo;
            player.Icon = data.Icon;
            player.tag = data.CharacterInfo.Tag; // Unnecessary. Just to see in inspector.

            return player;
        }

        private void CreateAlly(SpawnData data)
        {
            CharacterInfoController ally = Instantiate(allyPrefab);

            ally.transform.position = Vector3.zero; // TODO MAKE ENTER POINT IN DUNGEON
            ally.Info = data.CharacterInfo;
            ally.Icon = data.Icon;
        }
    }

    /// <summary>
    /// Data about current level.
    /// </summary>
    public class LevelCurrentData
    {
        /// <summary>
        /// Reference to player on scene. Can only be 1 player in scene.
        /// </summary>
        public CharacterInfoController PlayerReference;
    }
}