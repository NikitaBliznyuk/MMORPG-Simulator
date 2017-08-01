using Game.Character;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private CharacterInfoController characterPrefab;
    
    private void Awake()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        CharacterInfoController player = Instantiate(characterPrefab);
        CharacterInfoController.PlayerInstance = player;
        player.Abilities = AbilitiesParser.Instance.Abilities;
    }
}
