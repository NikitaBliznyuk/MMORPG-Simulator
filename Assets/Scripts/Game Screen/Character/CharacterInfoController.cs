using System.Collections;
using UnityEngine;

namespace Game.Character
{
    public class CharacterInfoController : MonoBehaviour
    {
        [SerializeField] private CharacterInfo info;
        [SerializeField] private SpriteRenderer view;

        private readonly CharacterState stateInfo = new CharacterState();

        public CharacterInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        public CharacterState StateInfo
        {
            get { return stateInfo; }
        }

        public float Size
        {
            get { return view.size.x / 2.0f; }
        }

        public Sprite Icon
        {
            set { view.sprite = value; }
        }

        public RangeVisualizer RangeVisualizer { get; set; }

        private IInputController inputController;

        private void Start()
        {
            name = info.StatsInfo.Name;
            inputController = GetComponent<IInputController>();
            
            StartCoroutine(StatRegeneration());
        }

        private IEnumerator StatRegeneration()
        {
            float currentTime = 0.0f;
            float gap = 0.25f;

            while (true) // TODO STOP CONDITION
            {
                if (currentTime >= gap)
                {
                    float healthIncreaseAmount = info.StatsInfo.HealthRegen * currentTime;
                    float energyIncreaseAmount = info.StatsInfo.EnergyRegen * currentTime;

                    info.StatsInfo.CurrentHealth += healthIncreaseAmount;
                    info.StatsInfo.CurrentEnergy += energyIncreaseAmount;

                    currentTime = 0.0f;
                }
                else
                {
                    currentTime += Time.deltaTime;
                }
                
                yield return null;   
            }
        }

        public void DealDamage(int damage)
        {
            damage = damage >= 0 ? damage : 0;
            info.StatsInfo.CurrentHealth -= damage;

            if ((int) info.StatsInfo.CurrentHealth == 0)
            {
                StateInfo.CurrentState = CharacterState.StateName.DEAD;
            }
        }

        public void Heal(int value)
        {
            value = value >= 0 ? value : 0;
            info.StatsInfo.CurrentHealth += value;
        }

        public AbilityInvokeErrorCode InvokeAbility(int index, CharacterInfoController target = null)
        {
            if(index > info.Abilities.Length - 1)
                return AbilityInvokeErrorCode.NO_SUCH_ABILITY;
            
            if(info.StatsInfo.CurrentEnergy < info.Abilities[index].AbilityInfo.Cost)
                return AbilityInvokeErrorCode.NO_ENERGY;
            
            CharacterInfoController invoker = this;
            if (target == null)
            {
                target = inputController != null && inputController.CurrentObservableInfo != null
                    ? inputController.CurrentObservableInfo.GetComponent<CharacterInfoController>()
                    : null;
            }

            if (Info.Abilities[index].Avaliable)
            {
                AbilityInvokeErrorCode code = Info.Abilities[index].Invoke(invoker, target);

                if (code == AbilityInvokeErrorCode.NO_ERROR)
                {
                    Info.StatsInfo.CurrentEnergy -= Info.Abilities[index].AbilityInfo.Cost;
                }
                else if (RangeVisualizer != null && code == AbilityInvokeErrorCode.TOO_FAR)
                {
                    RangeVisualizer.Visualize(Info.Abilities[index].AbilityInfo.CastDistance);
                }

                return code;
            }

            return AbilityInvokeErrorCode.NOT_AVALIABLE;
        }
    }
}
