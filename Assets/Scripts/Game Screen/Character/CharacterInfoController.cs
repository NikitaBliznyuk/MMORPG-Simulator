using UnityEngine;

namespace Game.Character
{
    public class CharacterInfoController : MonoBehaviour
    {
        [SerializeField] private CharacterInfo info;

        public CharacterInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        private IInputController inputController;

        private void Start()
        {
            name = info.StatsInfo.Name;
            inputController = GetComponent<IInputController>();
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

        public bool InvokeAbility(int index)
        {
            if(index > info.Abilities.Length - 1)
                return false;
            
            if(info.StatsInfo.CurrentEnergy < info.Abilities[index].AbilityInfo.Cost)
                return false;
            
            CharacterInfoController invoker = this;
            CharacterInfoController target = inputController.CurrentObservableInfo != null
                ? inputController.CurrentObservableInfo.GetComponent<CharacterInfoController>()
                : null;
            
            if (Info.Abilities[index].Avaliable)
            {
                bool successful = Info.Abilities[index].Invoke(invoker, target);
                if (successful)
                {
                    Info.StatsInfo.CurrentEnergy -= Info.Abilities[index].AbilityInfo.Cost;
                    Info.StatsInfo.CurrentEnergy = Info.StatsInfo.CurrentEnergy >= 0 ? Info.StatsInfo.CurrentEnergy : 0;
                }

                return successful;
            }

            return false;
        }
    }
}
