using System.Collections;
using UnityEngine;

namespace Game.Character
{
    public class CharacterInfoController : MonoBehaviour
    {
        [SerializeField] private CharacterInfo info;
        [SerializeField] private SpriteRenderer view;
        [SerializeField] private SpriteRenderer highlight;

        [SerializeField]
        private RangeVisualizer rangeVisualizer;

        private readonly CharacterState stateInfo = new CharacterState();
        private readonly Color deadColor = Color.gray;

        public CharacterInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        public CharacterState StateInfo
        {
            get { return stateInfo; }
        }

        public bool IsHighlighted
        {
            get { return highlight.enabled; }
            set
            {
                highlight.size = new Vector2(Size * 2.0f + 0.3f, Size * 2.0f + 0.3f);
                highlight.enabled = value;
            }
        }

        public float Size
        {
            get { return view != null ? view.sprite.bounds.extents.x : 0.0f; }
        }

        public Sprite Icon
        {
            set { view.sprite = value; }
        }

        public RangeVisualizer RangeVisualizer
        {
            get { return rangeVisualizer; }
        }

        private IInputController inputController;

        private void Start()
        {
            name = info.StatsInfo.Name;
            inputController = GetComponent<IInputController>();
            StateInfo.ChangeState += OnChangeState;
            
            StartCoroutine(StatRegeneration());
        }

        private void OnChangeState()
        {
            if (StateInfo.CurrentState == CharacterState.StateName.DEAD)
            {
                view.color = deadColor;
            }
        }

        private IEnumerator StatRegeneration()
        {
            float currentTime = 0.0f;
            float gap = 0.25f;

            while (StateInfo.CurrentState != CharacterState.StateName.DEAD)
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

        public void DealDamage(int value)
        {
            value = value >= 0 ? value : 0;
            info.StatsInfo.CurrentHealth -= value;

            if ((int) info.StatsInfo.CurrentHealth == 0)
            {
                StateInfo.CurrentState = CharacterState.StateName.DEAD;
            }

            if (PopupNumbersController.Instance != null)
                PopupNumbersController.Instance.CreateText(transform.position, Color.red, "-" + value);
        }

        public void Heal(int value)
        {
            value = value >= 0 ? value : 0;
            info.StatsInfo.CurrentHealth += value;

            if (PopupNumbersController.Instance != null)
                PopupNumbersController.Instance.CreateText(transform.position, Color.green, "+" + value);
        }

        public AbilityInvokeErrorCode InvokeAbility(int index, CharacterInfoController target = null)
        {
            if(index > info.Abilities.Length - 1)
                return AbilityInvokeErrorCode.NO_SUCH_ABILITY;
            
            if(info.StatsInfo.CurrentEnergy < info.Abilities[index].Ability.AbilityInfo.Cost)
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
                    Info.StatsInfo.CurrentEnergy -= Info.Abilities[index].Ability.AbilityInfo.Cost;
                }
                else if (RangeVisualizer != null && code == AbilityInvokeErrorCode.TOO_FAR)
                {
                    RangeVisualizer.Visualize(Info.Abilities[index].Ability.AbilityInfo.CastDistance + Size);
                }

                return code;
            }

            return AbilityInvokeErrorCode.NOT_AVALIABLE;
        }
    }
}
