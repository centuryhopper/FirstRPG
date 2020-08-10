using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandiMood : MonoBehaviour
{

    EnumExample enumExample;

    // Start is called before the first frame update
    void Start()
    {
        enumExample = GetComponent<EnumExample>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enumExample.brandiMoods = (EnumExample.BrandiMoods) Random.Range(0, 11);
        }
    }
}
