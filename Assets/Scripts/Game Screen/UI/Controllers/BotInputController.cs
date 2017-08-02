using System;
using System.Collections;
using System.Linq;
using Game.Character;
using UnityEngine;

// TODO REFACTOR THIS, MAKE INPUT CONTROLLER DO ONLY INPUTS

public class BotInputController : MonoBehaviour, IInputController
{
    private CharacterInfoController characterInfoController;
    
    public CharacterInfoController CurrentObservableInfo { get; private set; }
    public Vector3 NextPosition { get; private set; }

    private void Awake()
    {
        characterInfoController = GetComponent<CharacterInfoController>();
        NextPosition = transform.position;
    }

    private void Start()
    {
        CurrentObservableInfo = GameObject.FindGameObjectsWithTag(characterInfoController.Info.EnemyTag)
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
            .First()
            .GetComponent<CharacterInfoController>();

        StartCoroutine(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        while (CurrentObservableInfo.Info.StatsInfo.CurrentHealth > 0)
        {
            Ability avaliableAbility = characterInfoController.Info.Abilities.First(ability => ability is IHit);
            if (avaliableAbility != null)
            {
                int abilityIndex = Array.IndexOf(characterInfoController.Info.Abilities, avaliableAbility);
                Debug.Log(abilityIndex);
                characterInfoController.InvokeAbility(abilityIndex);
            }
            yield return null;
        }
    }
}
