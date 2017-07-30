using Game.UI.View;
using UnityEngine;
using UnityEngine.EventSystems;
using CharacterInfo = Game.Character.CharacterInfo;

public class ClickController : MonoBehaviour, IInputController
{
    [Header("References")]
    
    [SerializeField] private GameUiView view;

    public CharacterInfo CurrentObservableInfo { get; private set; }
    public Vector3 NextPosition { get; private set; }

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
                CurrentObservableInfo = hit.collider.GetComponentInParent<CharacterInfo>();
                view.UpdateTopUi(CurrentObservableInfo.StatsInfo, true);
            }
            else
            {
                GetNextPosition();
            }
        }
    }

    private bool IsClicking()
    {
        if (EventSystem.current.IsPointerOverGameObject(-1))
            return false;
        return Input.GetMouseButtonDown(0);
    }

    private void GetNextPosition()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        NextPosition = new Vector3(newPosition.x, newPosition.y, NextPosition.z);
    }
}
