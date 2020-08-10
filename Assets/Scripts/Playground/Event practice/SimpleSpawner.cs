using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
    [SerializeField] GameObject cylinderPrefab;
    float nextSpawnTime, respawnRate = 15;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + respawnRate;
        }
    }

    void Spawn()
    {
        var cylinder = Instantiate(cylinderPrefab, transform.position, Quaternion.identity) as GameObject;
    }
}
