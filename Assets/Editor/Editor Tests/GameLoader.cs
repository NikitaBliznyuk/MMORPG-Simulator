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
            Loader.StartLevel(data);
        }
    }
}
