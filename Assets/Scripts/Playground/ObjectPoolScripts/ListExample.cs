using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListExample : MonoBehaviour
{

    [SerializeField] List<int> lst = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lst.Add(Random.Range(1, 7));
        }
    }
}
