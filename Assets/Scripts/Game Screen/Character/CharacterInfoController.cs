using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class CharacterInfoController : MonoBehaviour
    {
        [Header("References")] 
        
        [SerializeField] private CharacterInfo info;
        [SerializeField] private GameObject clickControllerReference;

        [Header("Settings")] 
        
        [SerializeField] private string allyTag = "Ally";
        [SerializeField] private string enemyTag = "Enemy";

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
            Abilities = new List<IAbility> {new HitAbility(), new HealAbility()};
            if(clickControllerReference != null)
                inputController = clickControllerReference.GetComponent<IInputController>();
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
            CharacterInfoController invoker = this;
            CharacterInfoController target = inputController.CurrentObservableInfo != null
                ? inputController.CurrentObservableInfo.GetComponent<CharacterInfoController>()
                : null;
            Abilities[index].Invoke(invoker, target);
        }
    }
}
