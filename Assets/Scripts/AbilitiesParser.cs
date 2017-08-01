using System;
using System.Linq;
using UnityEngine;

public class AbilitiesParser
{
    private static readonly AbilitiesParser instance;

    public static AbilitiesParser Instance
    {
        get { return instance; }
    }

    static AbilitiesParser()
    {
        instance = new AbilitiesParser();
    }

    private AbilitiesParser()
    {
        Initialize();
    }

    public IAbility[] Abilities { get; private set; }

    private void Initialize()
    {
        string[] abilitiesJsons = Resources.Load<TextAsset>("Abilities").text
            .Split("&".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        Abilities = abilitiesJsons
            .Select(str =>
            {
                string className = str.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                string json = str.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
                Type classType = Type.GetType(className);
                IAbility ability = (IAbility) JsonUtility.FromJson(json, classType);
                return ability;
            })
            .ToArray();

        Debug.Log("Abilities count: " + Abilities.Length);
    }
}
