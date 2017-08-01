using UnityEditor;
using UnityEngine;

public class AbilitiesCreatorWindow : EditorWindow
{
    [MenuItem("Window/Abilities Creation")]
    public static void ShowWindow()
    {
        GetWindow<AbilitiesCreatorWindow>("Abilities Creation");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Hit ability json"))
        {
            LoadHitAbility();
        }

        if (GUILayout.Button("Heal ability json"))
        {
            LoadHealAbility();
        }

        if (GUILayout.Button("Load all abilities"))
        {
            string result = "";
            result += LoadHitAbility();
            result += LoadHealAbility();
            GUIUtility.systemCopyBuffer = result;
        }
    }

    private string LoadHitAbility()
    {
        IAbility ability = new HitAbility(
            new AbilityInfo
            {
                Name = "Hit",
                Description = "Simple hit",
                Cooldown = 1.0f,
                Cost = 5,
                Avaliable = true
            },
            new HitInfo
            {
                MinDamage = 10,
                MaxDamage = 15
            });
        string json = JsonUtility.ToJson(ability, true);
        string className = ability.GetType().FullName;
        string result = className + "|" + json + "&";
        return result;
    }

    private string LoadHealAbility()
    {
        IAbility ability = new HealAbility(new AbilityInfo
        {
            Name = "Heal",
            Description = "Simple heal",
            Cooldown = 1.5f,
            Cost = 7,
            Avaliable = true
        }, new HealInfo
        {
            MinHeal = 25,
            MaxHeal = 40
        });
        string json = JsonUtility.ToJson(ability, true);
        string className = ability.GetType().FullName;
        string result = className + "|" + json + "&";
        return result;
    }
}
