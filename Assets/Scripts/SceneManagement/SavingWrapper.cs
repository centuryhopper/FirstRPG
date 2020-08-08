using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "Save";
        [SerializeField] float fadeInTime = 0.5f;

        private void Awake()
        {
            // calling this co-routine in Awake() makes sure we load
            // the last saved scene before the Start() in BaseStats.cs
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            // Fader fader = FindObjectOfType<Fader>();
            // fader.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);

            // declaring and initializing it here will help avoid race condition from the
            // Awake() in persistentObjSpawner.cs
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        // loads the most recent saved state of the game
        public void Load()
        {
            // calls the load() in the SavingSystem.cs script
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        // saves the current state of the game
        public void Save()
        {
            // calls the save() in the SavingSystem.cs script
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        // deletes the current save file
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}
