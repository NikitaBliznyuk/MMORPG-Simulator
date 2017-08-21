using System.Collections.Generic;
using Game.Character;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickController : MonoBehaviour, IInputController
{
    public CharacterInfoController CurrentObservableInfo
    {
        get { return currentObservableInfo; }
        set
        {
            if(currentObservableInfo != null)
                currentObservableInfo.IsHighlighted = false;
            currentObservableInfo = value;
        }
    }
    
    private CharacterInfoController currentObservableInfo;
    private CharacterMovementController movementController;

    private void Start()
    {
        movementController = GetComponent<CharacterMovementController>();
    }

    private void Update()
    {
        if (IsClicking())
        {
            RaycastHit hit;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out hit, 20, LayerMask.GetMask("Clickable")))
            {
                CurrentObservableInfo = hit.collider.GetComponentInParent<CharacterInfoController>();
                CurrentObservableInfo.IsHighlighted = true;
            }
            else if (Physics.Raycast(cameraRay, out hit, 20, LayerMask.GetMask("Ground")))
            {
                movementController.MoveToPoint(hit.point);
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
}
