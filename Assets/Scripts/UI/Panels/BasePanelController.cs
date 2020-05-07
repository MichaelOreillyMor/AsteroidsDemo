using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI
{
    /// <summary>
    /// Just a small example of how UI react to game events. Not the most optimum design with coroutines and CanvasGroups
    /// </summary>
    public abstract class BasePanelController : MonoBehaviour
    {
        private const float FADE_IN_OUT_PANEL_DURATION = 1f;
        private const float FADE_PANEL_DURATION = 0.5f;

        [SerializeField]
        protected CanvasGroup canvasGroup;

        [SerializeField]
        protected Text message;

        protected void FadeIn()
        {
            StartCoroutine(Fade(0, 1));
        }

        protected void FadeOut()
        {
            StartCoroutine(Fade(1, 0));
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }

        protected void FadeInOut()
        {
            StartCoroutine(FadeInOutCo(FADE_IN_OUT_PANEL_DURATION));
        }

        private IEnumerator FadeInOutCo(float duration)
        {
            yield return StartCoroutine(Fade(0, 1));
            yield return new WaitForSeconds(duration);
            yield return StartCoroutine(Fade(1, 0));
        }

        // this should be done using Dotween 
        private IEnumerator Fade(float from, float to) 
        {
            for (float t = 0; t < FADE_PANEL_DURATION;)
            {
                t += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(from, to, t / FADE_PANEL_DURATION);
                yield return null;
            }

            canvasGroup.alpha = to;
        }
    }
}