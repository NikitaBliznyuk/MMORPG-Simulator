using UnityEngine;
using NUnit.Framework;
using Game.Character;
using Game.UI.View;
using CharacterInfo = Game.Character.CharacterInfo;

public class HealAbilityTest 
{
    [Test]
    public void InvokeNoTarget()
    {
        CharacterInfoController character = GenerateCharacter("Raid 0", "Raid 1");

        AbilityInvokeErrorCode code = character.InvokeAbility(0);
        Assert.AreEqual(AbilityInvokeErrorCode.NO_ERROR, code);
    }

    [Test]
    public void InvokeOnSelf()
    {
        CharacterInfoController character = GenerateCharacter("Raid 0", "Raid 1");

        AbilityInvokeErrorCode code = character.InvokeAbility(0, character);
        Assert.AreEqual(AbilityInvokeErrorCode.NO_ERROR, code);
    }
    
    [Test]
    public void InvokeOnAlly()
    {
        CharacterInfoController character1 = GenerateCharacter("Raid 0", "Raid 1");
        CharacterInfoController character2 = GenerateCharacter("Raid 0", "Raid 1");

        AbilityInvokeErrorCode code = character1.InvokeAbility(0, character2);
        Assert.AreEqual(AbilityInvokeErrorCode.NO_ERROR, code);
    }
    
    [Test]
    public void InvokeOnEnemy()
    {
        CharacterInfoController character1 = GenerateCharacter("Raid 0", "Raid 1");
        CharacterInfoController character2 = GenerateCharacter("Raid 1", "Raid 0");

        AbilityInvokeErrorCode code = character1.InvokeAbility(0, character2);
        Assert.AreEqual(AbilityInvokeErrorCode.NO_ERROR, code);
    }
    
    [Test]
    public void InvokeTooFar()
    {
        CharacterInfoController character1 = GenerateCharacter("Raid 0", "Raid 1");
        character1.transform.position = new Vector3(-10.0f, -10.0f, 0.0f);
        CharacterInfoController character2 = GenerateCharacter("Raid 0", "Raid 1");
        character2.transform.position = new Vector3(10.0f, 10.0f, 0.0f);

        AbilityInvokeErrorCode code = character1.InvokeAbility(0, character2);
        Assert.AreEqual(AbilityInvokeErrorCode.TOO_FAR, code);
    }
    
    [Test]
    public void InvokeNoEnergy()
    {
        CharacterInfoController character1 = GenerateCharacter("Raid 0", "Raid 1", 100, 0);
        CharacterInfoController character2 = GenerateCharacter("Raid 0", "Raid 1");

        AbilityInvokeErrorCode code = character1.InvokeAbility(0, character2);
        Assert.AreEqual(AbilityInvokeErrorCode.NO_ENERGY, code);
    }
    
    [Test]
    public void InvokeWrongAbilityIndex()
    {
        CharacterInfoController character1 = GenerateCharacter("Raid 0", "Raid 1");
        int previousHealth = (int) character1.Info.StatsInfo.CurrentHealth;

        AbilityInvokeErrorCode code = character1.InvokeAbility(10, character1);
        int afterHealingHealth = (int) character1.Info.StatsInfo.CurrentHealth;
        
        Assert.AreEqual(0, afterHealingHealth - previousHealth);
    }

    private CharacterInfoController GenerateCharacter(string allyTeam, string enemyTeam, float currentHealth = 100,
        float currentEnergy = 100)
    {
        GameObject o = new GameObject();
        CharacterInfoController character = o.AddComponent<CharacterInfoController>();
        character.Info = new CharacterInfo
        {
            Abilities = new Ability[] {GenerateHealAbility()},
            AllyTags = new[] {allyTeam},
            EnemyTags = new[] {enemyTeam},
            Tag = allyTeam,
            StatsInfo = new StatsInfo(100, currentHealth, 2, 40, currentEnergy, 3, "Character")
        };
        return character;
    }

    private HealAbility GenerateHealAbility()
    {
        HealAbility healAbility = ScriptableObject.CreateInstance<HealAbility>();

        AbilityInfo abilityInfo = healAbility.AbilityInfo;
        abilityInfo.Cost = 10;
        healAbility.AbilityInfo = abilityInfo;
        
        return healAbility;
    }
}
