using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates a reserved pool of objects
public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region singleton

// set it to itself
    public static ObjectPooler Instance;

    private void Awake () {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start ()
    {
        // instantiate a dictionary
        poolDictionary = new Dictionary<string, Queue<GameObject>> ();

        // ready the cube objects and disable them by default
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject> ();

            // pool.size is 100 for my case bc I set that in the inspector
            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate (pool.prefab);
                obj.SetActive (false);
                objectPool.Enqueue (obj);
            }

            poolDictionary.Add (pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        // codes defensively to check if user spelled the tag correctly
        if (!poolDictionary.ContainsKey (tag))
        {
            Debug.LogWarning ("Pool with tag" + tag + "dosen't exist");
            return null;
        }

        // Should not be empty because of Start() method populating it
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // make it active in the game
        if (objectToSpawn != null)
        {
            objectToSpawn.SetActive (true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        }

        // search for the interface component in the game object spawned, which happens to be
        // from the Cube.cs since that script implements IPooledObject, check that it's not null
        // and call its spawn method
        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        // Cube cube = objectToSpawn.GetComponent<Cube>();
// if (cube != null)
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
            // cube.OnObjectSpawn();
        }
        
        // once spawned, we want to 
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}