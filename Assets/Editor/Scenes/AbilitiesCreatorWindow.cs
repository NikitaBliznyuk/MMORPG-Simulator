using System;
using System.Collections;
using System.Collections.Generic;
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
            IAbility ability = new HitAbility(
                new AbilityInfo
                {
                    Name = "Hit",
                    Description = "Simple hit",
                    Cooldown = 1.0f,
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
            GUIUtility.systemCopyBuffer = result;
            Debug.Log(result);
        }
        
        if (GUILayout.Button("Heal ability json"))
        {
            IAbility ability = new HealAbility(new AbilityInfo
            {
                Name = "Heal",
                Description = "Simple heal",
                Cooldown = 1.5f,
                Avaliable = true
            }, new HealInfo
            {
                MinHeal = 25,
                MaxHeal = 40
            });
            string json = JsonUtility.ToJson(ability, true);
            string className = ability.GetType().FullName;
            string result = className + "|" + json + "&";
            GUIUtility.systemCopyBuffer = result;
            Debug.Log(result);
        }
    }
}
