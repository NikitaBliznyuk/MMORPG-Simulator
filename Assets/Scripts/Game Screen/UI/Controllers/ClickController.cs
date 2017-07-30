using Game.UI.View;
using UnityEngine;
using CharacterInfo = Game.Character.CharacterInfo;

public class ClickController : MonoBehaviour, IInputController
{
    [Header("References")]
    
    [SerializeField] private GameUiView view;

    public CharacterInfo CurrentObservableInfo { get; private set; }
    
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
        }
    }

    private bool IsClicking()
    {
        return Input.GetMouseButtonDown(0);
    }
}
