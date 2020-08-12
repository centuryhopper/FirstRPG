using System;
using UnityEngine;

// Spawns objects
public class DIspawner : MonoBehaviour
{
    [SerializeField] DirectReference cubePrefab = null;
    [SerializeField] ScoreManager0 scoreManager = null;
    [SerializeField] float respawnRate = 15;
    float nextSpawnTime = 0;
    

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Spawn();

            // ensures that a spawn happens only every 15 seconds
            nextSpawnTime = Time.time + respawnRate;
        }
    }

    private void Spawn()
    {
        // spawns a cube object
        DirectReference cube = Instantiate(cubePrefab, transform.position, Quaternion.identity);

        // dependency injection happens here
        cube.Initialize(scoreManager);
    }
}