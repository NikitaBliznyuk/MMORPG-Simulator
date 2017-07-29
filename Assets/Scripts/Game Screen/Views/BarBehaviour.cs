using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    public class BarBehaviour : MonoBehaviour
    {
        [SerializeField] private Image view;
        [SerializeField] private Text text;
        [SerializeField] private Gradient barGradient;
        
        public void UpdateBarInfo(int maxValue, int currentValue)
        {
            text.text = currentValue + "/" + maxValue;

            float t = currentValue / (float) maxValue;
            view.color = barGradient.Evaluate(t);
        }
    }
}