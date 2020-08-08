using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum PortalID
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneIndex = -1;
        [SerializeField] PortalID iD;
        [SerializeField] SceneParentLife s;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        // should not have to call StartCoroutine() inside a coroutine
        IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject.transform.parent);
            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(2.5f);

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            // save the current state of the world you are leaving
            savingWrapper.Save();
            // transition to the scene with 'sceneIndex' index
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            // load the the last state of the other world you are going to transition to
            savingWrapper.Load();

            print("scene loaded");

            Portal otherPortal = GetOtherPortal();
            UpdatePlayerToSpawnPoint(otherPortal);

            // do another save here so that the next time we load the game up
            // the player will appear by the portal where it last spawned from
            savingWrapper.Save();

            // stall for .5 seconds
            yield return new WaitForSeconds(0.5f);
            yield return fader.FadeIn(2.5f);


            // Destroy(gameObject);
            s.toToggleInPortalClass = true;

        }

        Portal GetOtherPortal()
        {
            // find the portal in the other scene we just transitioned to
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                // we don't want to return our own portal
                if (portal == this) {continue;}

                // we don't want to spawn at the wrong portal
                if (portal.iD != this.iD) {continue;}

                return portal;
            }

            return null;
        }

        void UpdatePlayerToSpawnPoint(Portal otherPortal)
        {
            if (otherPortal != null)
            {
                // should be in the other scene when calling this method
                Transform playerTransform = GameObject.FindWithTag("Player").transform;

                // toggling the navmesh agent component here is necessary to avoid bugs when
                // programmatically adjusting the player position and rotation
                playerTransform.GetComponent<NavMeshAgent>().enabled = false;

                // update player transform position and rotation
                playerTransform.position = otherPortal.transform.GetChild(0).transform.position;
                playerTransform.rotation = otherPortal.transform.GetChild(0).transform.rotation;

                playerTransform.GetComponent<NavMeshAgent>().enabled = true;

                // warps the navmesh agent to the spawn point to avoid conflicts in code logic
                playerTransform.GetComponent<NavMeshAgent>().Warp(otherPortal.transform.GetChild(0).transform.position);
            }
        }
    }
}
