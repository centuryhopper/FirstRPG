using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;

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
        [SerializeField] float waitTime = 10, fadeInTime = 0.5f, fadeOutTime = 0.5f;

        PlayerController playerController;
        public static event Action DestroyParentGameObj = delegate {  };

        private void Awake()
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

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

            // remove old player controller to avoid glitches
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();

            // save the current state of the world you are leaving
            savingWrapper.Save();
            // transition to the scene with 'sceneIndex' index
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            // theoretically removes new player controller to avoid glitches
            // TODO may need to call gameobject.find again for the new player
            // if this doesn't work (UPDATE: SO IT ACTUALLY DOESN'T WORK)
            // playerController.enabled = false;

            // find the playercontroller in the newly loaded scene
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.enabled = false;



            // load the the last state of the other world you are going to transition to
            savingWrapper.Load();

            print("scene loaded");

            Portal otherPortal = GetOtherPortal();
            UpdatePlayerToSpawnPoint(otherPortal);

            // do another save here so that the next time we load the game up
            // the player will appear by the portal where it last spawned from
            savingWrapper.Save();

            // stall for .5 seconds
            yield return new WaitForSeconds(waitTime);

            // runs in the background as a coroutine
            // so that the player controls can be re-enabled
            // asynchronously
            fader.FadeIn(fadeInTime);
            

            // theoretically restores new player player controller
            playerController.enabled = true;

            // destroys the parent gameobject
            // s.toToggleInPortalClass = true;
            // DestroyParentGameObj();
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
