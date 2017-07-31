using System.Collections.Generic;
using Game.Character;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesBarController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    
    private void Start()
    {
        List<IAbility> abilities = CharacterInfoController.PlayerInstance.Abilities;
        
        for (int i = 0; i < buttons.Length; i++)
        {
            Text text = buttons[i].GetComponentInChildren<Text>();
            text.text = i < abilities.Count ? abilities[i].AbilityInfo.Name : "?";
        }
    }
}
