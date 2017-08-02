using UnityEngine;

namespace Game.Character
{
    public class CharacterInfoController : MonoBehaviour
    {
        public CharacterInfo Info;

        [Header("Settings")] 
        
        [SerializeField] private string allyTag = "Ally";
        [SerializeField] private string enemyTag = "Enemy";
        
        public string AllyTag
        {
            get { return allyTag; }
        }
        public string EnemyTag
        {
            get { return enemyTag; }
        }

        private IInputController inputController;

        private void Start()
        {
            name = Info.StatsInfo.Name;
            inputController = GetComponent<IInputController>();
        }

        public void DealDamage(int damage)
        {
            damage = damage >= 0 ? damage : 0;
            Info.StatsInfo.CurrentHealth -= damage;
            Info.StatsInfo.CurrentHealth = Info.StatsInfo.CurrentHealth >= 0 ? Info.StatsInfo.CurrentHealth : 0;
        }

        public void Heal(int value)
        {
            value = value >= 0 ? value : 0;
            Info.StatsInfo.CurrentHealth += value;
            Info.StatsInfo.CurrentHealth = Info.StatsInfo.CurrentHealth <= Info.StatsInfo.MaxHealth
                ? Info.StatsInfo.CurrentHealth
                : Info.StatsInfo.MaxHealth;
        }

        public void InvokeAbility(int index)
        {
            if(index > Info.Abilities.Length - 1)
                return;
            
            if(Info.StatsInfo.CurrentEnergy < Info.Abilities[index].AbilityInfo.Cost)
                return;
            
            CharacterInfoController invoker = this;
            CharacterInfoController target = inputController.CurrentObservableInfo != null
                ? inputController.CurrentObservableInfo.GetComponent<CharacterInfoController>()
                : null;
            
            if (Info.Abilities[index].Avaliable)
            {
                Info.Abilities[index].Invoke(invoker, target);
                Info.StatsInfo.CurrentEnergy -= Info.Abilities[index].AbilityInfo.Cost;
                Info.StatsInfo.CurrentEnergy = Info.StatsInfo.CurrentEnergy >= 0 ? Info.StatsInfo.CurrentEnergy : 0;
            }
        }
    }
}
