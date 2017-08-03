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
            data.Player.StatsInfo.CurrentHealth = data.Player.StatsInfo.MaxHealth;
            data.Player.StatsInfo.CurrentEnergy = data.Player.StatsInfo.MaxEnergy;
            foreach (var playerAbility in data.Player.Abilities)
            {
                playerAbility.Avaliable = true;
            }

            foreach (var enemy in data.Enemies)
            {
                enemy.StatsInfo.CurrentHealth = enemy.StatsInfo.MaxHealth;
                enemy.StatsInfo.CurrentEnergy = enemy.StatsInfo.MaxEnergy;
                foreach (var enemyAbility in enemy.Abilities)
                {
                    enemyAbility.Avaliable = true;
                }
            }
            
            Loader.StartLevel(data);
        }
    }
}
