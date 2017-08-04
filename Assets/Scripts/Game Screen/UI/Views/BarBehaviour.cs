using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.View
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
            maxValue = Mathf.Max(maxValue, 0);
            currentValue = Mathf.Clamp(currentValue, 0, maxValue);
            
            text.text = currentValue + "/" + maxValue;

            float t = maxValue > 0.0f ? currentValue / (float) maxValue : 0.0f;
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