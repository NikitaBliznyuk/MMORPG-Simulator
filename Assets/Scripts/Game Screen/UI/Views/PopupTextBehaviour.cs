using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PopupTextBehaviour : MonoBehaviour
{
    [Header("Settings")] 
    
    [SerializeField] private AnimationCurve alphaOnTime;
    [SerializeField] private AnimationCurve ySpeedOnTime;
    [SerializeField] private float duration;

    private Text textContainer;

    private void Awake()
    {
        textContainer = GetComponent<Text>();
    }

    public void Show(string text, Color color, Vector2 position)
    {
        transform.position = position;
        textContainer.text = text;
        textContainer.color = color;
        
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        float currentTime = 0.0f;
        Color color = textContainer.color;
        color.a = 1.0f;
        textContainer.color = color;

        while (currentTime < duration)
        {
            float t = currentTime / duration;
            
            color = textContainer.color;
            color.a = alphaOnTime.Evaluate(t);
            textContainer.color = color;
            transform.position += Vector3.up * ySpeedOnTime.Evaluate(t) * Time.deltaTime;
            
            currentTime += Time.deltaTime;
            yield return null;
        }

        color = textContainer.color;
        color.a = 0.0f;
        textContainer.color = color;

        transform.SetParent(PopupNumbersController.Instance.transform);
        PopupNumbersController.Instance.TextPool.Push(this);
    }
}
