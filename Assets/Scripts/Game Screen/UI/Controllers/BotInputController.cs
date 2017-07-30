using System.Collections;
using System.Linq;
using Game.Character;
using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

// TODO REFACTOR THIS, MAKE INPUT CONTROLLER DO ONLY INPUTS

public class BotInputController : MonoBehaviour, IInputController
{
    private CharacterInfoController characterInfoController;
    
    public CharacterInfo CurrentObservableInfo { get; private set; }
    public Vector3 NextPosition { get; private set; }

    private void Awake()
    {
        characterInfoController = GetComponent<CharacterInfoController>();
        NextPosition = transform.position;
    }

    private void Start()
    {
        CurrentObservableInfo = GameObject.FindGameObjectsWithTag(characterInfoController.EnemyTag)
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position))
            .First()
            .GetComponent<CharacterInfo>();

        StartCoroutine(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        while (CurrentObservableInfo.StatsInfo.CurrentHealth > 0)
        {
            characterInfoController.InvokeAbility(0);
            yield return null;
        }
    }
}
