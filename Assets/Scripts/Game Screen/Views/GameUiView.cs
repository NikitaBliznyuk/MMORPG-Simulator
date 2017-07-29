using UnityEngine;

namespace Game.View
{
    public class GameUiView : MonoBehaviour
    {
        [SerializeField] private UiBehaviour topUI;

        private void Start()
        {
            topUI.gameObject.SetActive(false);
        }

        public void UpdateTopUi(UiInfo info, bool active)
        {
            topUI.gameObject.SetActive(active);
            
            
        }
    }

    public struct UiInfo
    {
        public int Health;
        public int Energy;
        public string Name;
    }
}
