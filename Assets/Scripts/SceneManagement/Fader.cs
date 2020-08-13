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

        // target = 1 for fadeout and target = 0 for fadein
        public IEnumerator FadeRoutine(float target, float fadeTime)
        {
            // move alpha towards target
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                // deltatime over the given time (in seconds) we
                // want the game to take fading
                canvasGroup.alpha = 
                    Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / fadeTime);

                // delays the coroutine from running by one frame
                yield return null;
            }
        }

        Coroutine currentlyActiveFade = null;
        public Coroutine FadeCheck(float target, float fadeTime)
        {
            // stop the currently active coroutine, if any
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }

            // assign and run the new coroutine
            currentlyActiveFade = StartCoroutine(FadeRoutine(target, fadeTime));

            return currentlyActiveFade;
        }

        // avoids race condition between fadeout and fade in
        // can yield return coroutine, null, 0, ienumerators, waitforseconds....
        public Coroutine FadeOut(float fadeTime)
        {
            return FadeCheck(1, fadeTime);
        }

        public Coroutine FadeIn(float fadeTime)
        {
            return FadeCheck(0, fadeTime);           
        }
        









        // This is a coroutine that runs other coroutines (nested concept),
        // and coroutines, in general, can have as many 'yield return' statements as desired
        // IEnumerator FadeOutThenFadeIn()
        // {
        //     yield return FadeOut(fadingTime);
        //     print("faded out and delaying for 3 seconds");

        //     // yield return new WaitForSeconds(3f);
            
        //     yield return FadeIn(fadingTime);
        //     print("faded back in");
        // }

        // yield return null and yield return 0 delays the coroutine from running by one frame
    }
}
