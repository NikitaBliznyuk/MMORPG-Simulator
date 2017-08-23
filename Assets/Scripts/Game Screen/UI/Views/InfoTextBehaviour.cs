using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameScreen.UI.View
{
    public class InfoTextBehaviour : MonoBehaviour
    {
        public static InfoTextBehaviour Instance;

        [Header("References")]

        [SerializeField]
        private Text infoText;

        [Header("Settings")]

        [SerializeField]
        private float fadeDuration;

        [SerializeField]
        private float messageDuration;

        private Coroutine currentAnimation;

        private void Awake()
        {
            Instance = this;

            Color color = infoText.color;
            color.a = 0.0f;
            infoText.color = color;
        }

        public void ShowMessage(string text)
        {
            if (currentAnimation != null)
                StopCoroutine(currentAnimation);

            infoText.text = text;

            currentAnimation = StartCoroutine(Animation());
        }

        private IEnumerator Animation()
        {
            Color color = infoText.color;
            float currentTime = color.a * fadeDuration;

            while (currentTime < fadeDuration)
            {
                color = infoText.color;
                color.a = currentTime / fadeDuration;
                infoText.color = color;

                currentTime += Time.deltaTime;
                yield return null;
            }

            color = infoText.color;
            color.a = 1.0f;
            infoText.color = color;

            yield return new WaitForSeconds(messageDuration);

            currentTime = 0.0f;

            while (currentTime < fadeDuration)
            {
                color = infoText.color;
                color.a = 1.0f - currentTime / fadeDuration;
                infoText.color = color;

                currentTime += Time.deltaTime;
                yield return null;
            }

            color = infoText.color;
            color.a = 0.0f;
            infoText.color = color;
        }
    }
}