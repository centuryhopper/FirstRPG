using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        float fadingTime = 2.5f;


        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
        }

        public void FadeOutImmediately()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1;
            }
        }

        public IEnumerator FadeOut(float fadeTime)
        {
            print("lerping alpha");

            // move alpha towards 1
            while (canvasGroup.alpha < 1f)
            {
                // deltatime over the given time (in seconds) we want the game to take fading
                canvasGroup.alpha += Time.deltaTime / fadeTime;

                // delays the coroutine from running by one frame
                yield return null;
            }
        }

        public IEnumerator FadeIn(float fadeTime)
        {
            print("faded in");

            if (canvasGroup == null) {yield break;}

            // move alpha towards 1
            while (canvasGroup.alpha > 0f)
            {
                // deltatime over the given time (in seconds) we want the game to take fading
                canvasGroup.alpha -= Time.deltaTime / fadeTime;

                // delays the coroutine from running by one frame
                yield return null;
            }
        }

        // This is a coroutine that runs other coroutines (nested concept),
        // and coroutines, in general, can have as many 'yield return' statements as desired
        IEnumerator FadeOutThenFadeIn()
        {
            yield return FadeOut(fadingTime);
            print("faded out and delaying for 3 seconds");

            // yield return new WaitForSeconds(3f);

            
            yield return FadeIn(fadingTime);
            print("faded back in");
        }

        // yield return null delays the coroutine from running by one frame
    }
}
