using System.Collections;
using GameScreen.Character;
using GameScreen.Character.Abilities;
using GameScreen.Loader;
using GameScreen.UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace GameScreen.UI.Controllers
{
    /// <summary>
    /// Controlls all buttons, that asign to it.
    /// </summary>
    public class AbilitiesBarController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Ability buttons")]
        private Button[] buttons;

        /// <summary>
        /// Current level data reference.
        /// </summary>
        private LevelCurrentData currentData;

        private void Awake()
        {
            Loader.Loader.DataUpdated += LoaderOnDataUpdated;
        }

        private void Update()
        {
            if(currentData == null)
                return;
            
            for (int i = 0; i < currentData.PlayerReference.Info.Abilities.Length; i++)
            {
                bool enoughMana = currentData.PlayerReference.Info.StatsInfo.CurrentEnergy >
                                  currentData.PlayerReference.Info.Abilities[i].Ability.AbilityInfo.Cost;

                buttons[i].GetComponent<Image>().color = enoughMana ? Color.white : Color.blue;
            }
        }

        /// <summary>
        /// Invoked by Loader.DataUpdated event.
        /// </summary>
        /// <param name="data">Current level data.</param>
        private void LoaderOnDataUpdated(LevelCurrentData data)
        {
            currentData = data;
            
            AbilityContainer[] abilities = data.PlayerReference.Info.Abilities;

            for (int i = 0; i < buttons.Length; i++)
            {
                Text text = buttons[i].GetComponentInChildren<Text>();
                int index = i;
                if (i < abilities.Length)
                {
                    buttons[i].onClick.AddListener(() =>
                    {
                        AbilityInvokeErrorCode code = data.PlayerReference.InvokeAbility(index);

                        if (code == AbilityInvokeErrorCode.NO_ERROR)
                        {
                            buttons[index].interactable = false;
                            StartCoroutine(
                                ButtonCooldown(buttons[index], abilities[index].Ability.AbilityInfo.Cooldown));
                        }
                        else
                        {
                            switch (code)
                            {
                                case AbilityInvokeErrorCode.TOO_FAR:
                                    InfoTextBehaviour.Instance.ShowMessage("Target is too far.");
                                    break;
                                case AbilityInvokeErrorCode.NO_ENERGY:
                                    InfoTextBehaviour.Instance.ShowMessage("Not enough energy.");
                                    break;
                                case AbilityInvokeErrorCode.NOT_AVALIABLE:
                                    InfoTextBehaviour.Instance.ShowMessage("Not avaliable yet.");
                                    break;
                                case AbilityInvokeErrorCode.WRONG_TARGET:
                                    InfoTextBehaviour.Instance.ShowMessage("Wrong target.");
                                    break;
                            }
                        }
                    });
                    text.text = abilities[i].Ability.AbilityInfo.Name;
                }
                else
                {
                    text.text = "?";
                }
            }
        }

        /// <summary>
        /// Cooldown coroutine.
        /// </summary>
        /// <param name="button">Button, that need to be cooldown.</param>
        /// <param name="duration">Cooldown duration.</param>
        /// <returns></returns>
        private IEnumerator ButtonCooldown(Button button, float duration)
        {
            float currentTime = 0.0f;
            Image buttonImage = button.GetComponent<Image>();
            buttonImage.fillAmount = 0.0f;

            while (currentTime < duration)
            {
                buttonImage.fillAmount = currentTime / duration;

                currentTime += Time.deltaTime;
                yield return null;
            }

            buttonImage.fillAmount = 1.0f;
            button.interactable = true;
        }
    }
}