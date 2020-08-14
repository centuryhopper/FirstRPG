using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullableTypesExamples : MonoBehaviour
{
    [SerializeField] string myName;
    [SerializeField] int myAge, myHeight;

    void CreateUser(string name, int? age, int? height)
    {
        // need temporary variables to help check for null --> (??)
        string a = name == String.Empty ? "jack the ripper" : name;
        Debug.Log("name: " + a);

        int b = age ?? 0;
        Debug.Log("age: " + b + " years old");

        int c = height ?? 0;
        Debug.Log("height: " + c + " inches");
    }

    // Start is called before the first frame update
    void Start()
    {
        // CreateUser("Brandi", 19, 63);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     CreateUser(myName, myAge, myHeight);
        // }
    }
}
