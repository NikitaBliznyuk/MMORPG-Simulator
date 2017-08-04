using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesBarController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        Loader.DataUpdated += LoaderOnDataUpdated;
    }

    private void LoaderOnDataUpdated(LevelCurrentData data)
    {
        Ability[] abilities = data.PlayerReference.Info.Abilities;
        
        for (int i = 0; i < buttons.Length; i++)
        {
            Text text = buttons[i].GetComponentInChildren<Text>();
            if (i < abilities.Length)
            {
                int index = i;
                buttons[i].onClick.AddListener(() =>
                {
                    AbilityInvokeErrorCode code = data.PlayerReference.InvokeAbility(index);
                    
                    if (code == AbilityInvokeErrorCode.NO_ERROR)
                    {
                        buttons[index].interactable = false;
                        StartCoroutine(ButtonCooldown(buttons[index], abilities[index].AbilityInfo.Cooldown));
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
                text.text = abilities[i].AbilityInfo.Name;
            }
            else
            {
                text.text = "?";
            }
        }
    }

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
