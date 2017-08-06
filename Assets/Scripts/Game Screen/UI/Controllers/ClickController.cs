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

    public Vector3 NextPosition { get; private set; }

    private CharacterInfoController currentObservableInfo;

    private void Awake()
    {
        NextPosition = transform.position;
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
                CurrentObservableInfo.IsHighlighted = true;
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
