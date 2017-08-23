using UnityEngine;
using NUnit.Framework;
using GameScreen.Character;
using CharacterInfo = GameScreen.Character.CharacterInfo;

public class CharacterStateTest 
{
    [Test]
    public void BecomeDeadFieldChangedToDead()
    {
        CharacterInfoController character = GenerateCharacter("Raid 0", "Raid 1", 2, 0);
        character.DealDamage(10);
        
        Assert.AreEqual(CharacterState.StateName.DEAD, character.StateInfo.CurrentState);
    }

    [Test]
    public void BecomeDeadRecieveEvent()
    {
        CharacterInfoController character = GenerateCharacter("Raid 0", "Raid 1", 2, 0);

        bool recieved = false;

        character.StateInfo.ChangeState += () => recieved = true;
        character.DealDamage(10);
        
        Assert.AreEqual(true, recieved);
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
