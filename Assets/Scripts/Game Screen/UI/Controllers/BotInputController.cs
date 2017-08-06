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

    // TODO REMOVE AFTER TESTING
    private void Start()
    {
        CurrentObservableInfo = GameObject.FindGameObjectsWithTag(characterInfoController.Info.EnemyTags.First())
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
            .First()
            .GetComponent<CharacterInfoController>();

        StartCoroutine(TestCoroutine());
        
        characterInfoController.StateInfo.ChangeState += OnChangeState;
    }

    private void OnChangeState()
    {
        if(characterInfoController.StateInfo.CurrentState == CharacterState.StateName.DEAD)
            enabled = false;
    }

    // TODO REMOVE AFTER TESTING
    private IEnumerator TestCoroutine()
    {
        while (characterInfoController.StateInfo.CurrentState != CharacterState.StateName.DEAD)
        {
            Ability avaliableAbility = characterInfoController.Info.Abilities.First(ability => ability is IHit);
            if (avaliableAbility != null)
            {
                int abilityIndex = Array.IndexOf(characterInfoController.Info.Abilities, avaliableAbility);
                characterInfoController.InvokeAbility(abilityIndex, CurrentObservableInfo);
            }
            yield return null;
        }
    }
}
