using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistentObj;

    // subsitute for the singleton pattern
    static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned) {return;}

        SpawnPersistentObjects();

        // this bool will stay true across all instances of this class
        hasSpawned = true;
    }

    private void SpawnPersistentObjects()
    {
        // make sure that the the persistent gameobject doesn't get destroyed between scenes
        DontDestroyOnLoad(Instantiate(persistentObj));
    }
}
