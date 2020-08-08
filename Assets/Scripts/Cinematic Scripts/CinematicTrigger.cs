using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // makes sure that the only character allowed to trigger the animation
            // is the player
            if (other.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                StartCoroutine(LetDaDamnCinematicPlayThrough(15f));
            }
        }

        IEnumerator LetDaDamnCinematicPlayThrough(float seconds)
        {
            WaitForSeconds numSeconds = new WaitForSeconds(seconds);
            yield return numSeconds;
            // Debug.Log("time has elapsed");
            gameObject.SetActive(false);
        }
    }
}
