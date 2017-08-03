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
    
    private CharacterInfoController GenerateCharacter(string allyTeam, string enemyTeam)
    {
        GameObject o = new GameObject();
        CharacterInfoController character = o.AddComponent<CharacterInfoController>();
        character.Info = new CharacterInfo
        {
            Abilities = new Ability[] {GenerateHealAbility()},
            AllyTags = new[] {allyTeam},
            EnemyTags = new[] {enemyTeam},
            Tag = allyTeam,
            StatsInfo = new StatsInfo(100, 100, 2, 40, 40, 3, "Player 1")
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
