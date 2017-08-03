using UnityEditor;
using UnityEngine;

public class GameLoader : EditorWindow
{
    private LevelData data;
    
    [MenuItem("Window/Editor Tests/Game Loader")]
    public static void ShowWindow()
    {
        GetWindow<GameLoader>("Game Loader");
    }

    private void OnGUI()
    {
        data = (LevelData) EditorGUILayout.ObjectField("Label:", data, typeof(LevelData), false);
        
        if (GUILayout.Button("Load game"))
        {
            data.Player.CharacterInfo.StatsInfo.CurrentHealth = data.Player.CharacterInfo.StatsInfo.MaxHealth;
            data.Player.CharacterInfo.StatsInfo.CurrentEnergy = data.Player.CharacterInfo.StatsInfo.MaxEnergy;
            foreach (var playerAbility in data.Player.CharacterInfo.Abilities)
            {
                playerAbility.Avaliable = true;
            }

            foreach (var enemy in data.Enemies)
            {
                enemy.CharacterInfo.StatsInfo.CurrentHealth = enemy.CharacterInfo.StatsInfo.MaxHealth;
                enemy.CharacterInfo.StatsInfo.CurrentEnergy = enemy.CharacterInfo.StatsInfo.MaxEnergy;
                foreach (var enemyAbility in enemy.CharacterInfo.Abilities)
                {
                    enemyAbility.Avaliable = true;
                }
            }
            
            Loader.StartLevel(data);
        }
    }
}
