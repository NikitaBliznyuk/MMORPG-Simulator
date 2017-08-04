using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RangeVisualizer : MonoBehaviour
{
    [Header("Settings")] 
    
    [SerializeField] private AnimationCurve alphaOnTime;
    [SerializeField] private float duration;

    private SpriteRenderer rangeRenderer;
    private Coroutine currentAnimation;

    private void Awake()
    {
        rangeRenderer = GetComponent<SpriteRenderer>();
        rangeRenderer.size = new Vector2(0.0f, 0.0f);
    }

    public void Visualize(float radius)
    {
        rangeRenderer.size = new Vector2(radius * 2.0f, radius * 2.0f);
        
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);
        currentAnimation = StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        float currentTime = 0.0f;

        while (currentTime < duration)
        {
            Color currentColor = rangeRenderer.color;
            currentColor.a = alphaOnTime.Evaluate(currentTime / duration);
            rangeRenderer.color = currentColor;
            
            currentTime += Time.deltaTime;
            yield return null;
        }

        Color color = rangeRenderer.color;
        color.a = 0.0f;
        rangeRenderer.color = color;
    }
}
