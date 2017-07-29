using Game.UI.View;
using UnityEngine;
using CharacterInfo = Game.CharacterInfo;

public class ClickController : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField] private GameUiView view;
    
    private void Update()
    {
        if (IsClicking())
        {
            RaycastHit hit;
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, out hit, 10, LayerMask.GetMask("Clickable")))
            {
                CharacterInfo info = hit.collider.GetComponentInParent<CharacterInfo>();
                view.UpdateTopUi(info.StatsInfo, true);
            }
            else
            {
                view.UpdateTopUi(new UiInfo(), false);
            }
        }
    }

    private bool IsClicking()
    {
        return Input.GetMouseButtonDown(0);
    }
}
