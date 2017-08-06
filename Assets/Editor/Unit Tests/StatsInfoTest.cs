using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Game.Character;
using Game.UI.View;
using CharacterInfo = Game.Character.CharacterInfo;

public class StatsInfoTest 
{
    [Test]
    public void CurrentHealthOverflow()
    {
        CharacterInfoController character = GenerateCharacter("Raid 0", "Raid 1");

        float healthBeforeHealing = character.Info.StatsInfo.CurrentHealth;
        character.Heal(100);
        float healthAfterHealing = character.Info.StatsInfo.CurrentHealth;
        
        Assert.AreEqual(0, healthAfterHealing - healthBeforeHealing);
    }
    
    [Test]
    public void CurrentHealthHealOn10()
    {
        CharacterInfoController character = GenerateCharacter("Raid 0", "Raid 1", 75);

        float healthBeforeHealing = character.Info.StatsInfo.CurrentHealth;
        character.Heal(10);
        float healthAfterHealing = character.Info.StatsInfo.CurrentHealth;
        
        Assert.AreEqual(10, healthAfterHealing - healthBeforeHealing);
    }
    
    [Test]
    public void CurrentHealthBelow0()
    {
        CharacterInfoController character = GenerateCharacter("Raid 0", "Raid 1");
        
        character.DealDamage(1000);
        float healthAfterHealing = character.Info.StatsInfo.CurrentHealth;
        
        Assert.AreEqual(0, healthAfterHealing);
    }
    
    private CharacterInfoController GenerateCharacter(string allyTeam, string enemyTeam, float currentHealth = 100,
        float currentEnergy = 100)
    {
        GameObject o = new GameObject();
        CharacterInfoController character = o.AddComponent<CharacterInfoController>();
        character.Info = new CharacterInfo
        {
            Abilities = new AbilityContainer[] {},
            AllyTags = new[] {allyTeam},
            EnemyTags = new[] {enemyTeam},
            Tag = allyTeam,
            StatsInfo = new StatsInfo(100, currentHealth, 2, 40, currentEnergy, 3, "Character")
        };
        return character;
    }
}
