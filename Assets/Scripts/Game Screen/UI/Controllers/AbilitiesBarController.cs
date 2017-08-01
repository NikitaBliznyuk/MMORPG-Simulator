using Game.Character;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesBarController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    
    private void Start()
    {
        IAbility[] abilities = CharacterInfoController.PlayerInstance.Abilities;
        
        for (int i = 0; i < buttons.Length; i++)
        {
            Text text = buttons[i].GetComponentInChildren<Text>();
            if (i < abilities.Length)
            {
                int index = i;
                buttons[i].onClick.AddListener(() =>
                {
                    CharacterInfoController.PlayerInstance.InvokeAbility(index);
                });
                text.text = abilities[i].AbilityInfo.Name;
            }
        }
    }
}
