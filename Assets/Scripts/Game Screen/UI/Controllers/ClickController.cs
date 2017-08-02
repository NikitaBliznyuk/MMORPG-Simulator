using System.Collections.Generic;
using Game.Character;
using Game.UI.View;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour, IInputController
{
    public CharacterInfoController CurrentObservableInfo { get; private set; }
    public Vector3 NextPosition { get; private set; }
    
    private GameUiView view;

    private void Awake()
    {
        NextPosition = transform.position;
        view = FindObjectOfType<GameUiView>();
    }

    private void Update()
    {
        if (IsClicking())
        {
            RaycastHit hit;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out hit, 10, LayerMask.GetMask("Clickable")))
            {
                CurrentObservableInfo = hit.collider.GetComponentInParent<CharacterInfoController>();
                view.UpdateTopUi(CurrentObservableInfo.Info.StatsInfo, true);
            }
            else
            {
                GetNextPosition();
            }
        }
    }

    private bool IsClicking()
    {
        return !IsPointerOverUIObject() && Input.GetMouseButtonDown(0);
    }
    
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition =
            new PointerEventData(EventSystem.current)
            {
                position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
            };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void GetNextPosition()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        NextPosition = new Vector3(newPosition.x, newPosition.y, NextPosition.z);
    }
}
