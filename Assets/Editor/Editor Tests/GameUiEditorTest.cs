using Game.UI.View;
using TestNamespace;
using UnityEditor;
using UnityEngine;

public class GameUiEditorTest : EditorWindow
{
    [MenuItem("Window/Editor Tests/Game UI Test")]
    public static void ShowWindow()
    {
        GetWindow<GameUiEditorTest>("Game UI Test");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Update UI Info"))
        {
            if (!Application.isPlaying)
            {
                Debug.LogWarning("Start game to execute this test.");
                return;
            }
            var gameUI = FindObjectOfType<GameUiView>();
            if (gameUI != null)
            {
                gameUI.UpdateTopUi(new UiInfo {MaxHealth = 100, CurrentHealth = 40, Name = "John Cena"}, true);
            }
            else
            {
                Debug.LogWarning("Coudn't find any game ui in scene. Try to switch scene or add game ui.");
            }
        }
        
        // TODO REMOVE THIS TEST
        if (GUILayout.Button("Test json"))
        {
            IAbility ability = new TestAbility(new AbilityInfo
            {
                Name = "Test",
                Avaliable = true,
                Cooldown = 0.5f,
                Description = "Hello"
            }, new HitInfo
            {
                MinDamage = 10,
                MaxDamage = 15
            });
            string className = ability.ClassName;
            string json = JsonUtility.ToJson(ability);
            string result = className + "|" + json;
            Debug.Log(result);
        }
    }
}
