using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// puts the object pooler into action
public class CubeSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        objectPooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);  
    }
}
