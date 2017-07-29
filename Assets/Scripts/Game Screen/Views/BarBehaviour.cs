using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class BarBehaviour : MonoBehaviour
    {
        [SerializeField] private Image view;
        [SerializeField] private Text text;
        [SerializeField] private Gradient barGradient;

        private RectTransform viewTransform;

        private void Awake()
        {
            viewTransform = view.GetComponent<RectTransform>();
        }

        public void UpdateBarInfo(int maxValue, int currentValue)
        {
            text.text = currentValue + "/" + maxValue;

            float t = currentValue / (float) maxValue;
            view.color = barGradient.Evaluate(t);
            
            MoveView(t);
        }

        private void MoveView(float t)
        {
            Vector3 currentPosition = viewTransform.localPosition;
            viewTransform.localPosition = new Vector3
                (- (1.0f - t) * viewTransform.rect.width,
                currentPosition.y,
                currentPosition.z);
        }
    }
}