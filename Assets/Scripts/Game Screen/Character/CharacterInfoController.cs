using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class CharacterInfoController : MonoBehaviour
    {
        public static CharacterInfoController PlayerInstance;
        
        [Header("References")] 
        
        [SerializeField] private CharacterInfo info;

        [Header("Settings")] 
        
        [SerializeField] private string allyTag = "Ally";
        [SerializeField] private string enemyTag = "Enemy";

        [SerializeField] private List<AbilityInfo> infos;

        public List<IAbility> Abilities { get; private set; }
        public string AllyTag
        {
            get { return allyTag; }
        }
        public string EnemyTag
        {
            get { return enemyTag; }
        }

        private IInputController inputController;

        private void Awake()
        {
            if (name == "Player")
            {
                PlayerInstance = this;
            }
            
            LoadAbilities();
            inputController = GetComponent<IInputController>();
        }

        private void LoadAbilities()
        {
            // TODO REMOVE THIS WITH LOADING INFO FROM MENU
            Abilities = new List<IAbility>();
            /*foreach (var abilityInfo in infos)
            {
                Type abilityType = Type.GetType(abilityInfo.ClassName);
                
                if (abilityType != null)
                {
                    IAbility ability = (IAbility) Activator.CreateInstance(abilityType, abilityInfo);
                    Abilities.Add(ability);
                }
            }*/
        }

        public void DealDamage(int damage)
        {
            damage = damage >= 0 ? damage : 0;
            info.StatsInfo.CurrentHealth -= damage;
            info.StatsInfo.CurrentHealth = info.StatsInfo.CurrentHealth >= 0 ? info.StatsInfo.CurrentHealth : 0;
        }

        public void Heal(int value)
        {
            value = value >= 0 ? value : 0;
            info.StatsInfo.CurrentHealth += value;
            info.StatsInfo.CurrentHealth = info.StatsInfo.CurrentHealth <= info.StatsInfo.MaxHealth
                ? info.StatsInfo.CurrentHealth
                : info.StatsInfo.MaxHealth;
        }

        public void InvokeAbility(int index)
        {
            if(index > Abilities.Count - 1)
                return;
            
            CharacterInfoController invoker = this;
            CharacterInfoController target = inputController.CurrentObservableInfo != null
                ? inputController.CurrentObservableInfo.GetComponent<CharacterInfoController>()
                : null;
            if(Abilities[index].AbilityInfo.Avaliable)
                Abilities[index].Invoke(invoker, target);
        }
    }
}
