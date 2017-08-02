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
                    data.PlayerReference.InvokeAbility(index);
                });
                text.text = abilities[i].AbilityInfo.Name;
            }
            else
            {
                text.text = "?";
            }
        }
    }
}
