using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct Person
{
    public string name;
    public string[] hobbies;

    [Tooltip("in years")]
    public int age;

    [Tooltip("in inches")]
    public int height;
}


public class StructExample : MonoBehaviour
{

    [SerializeField] Person brandi;
    // Start is called before the first frame update
    void Start()
    {
        brandi.name = "Brandi Ashton Boyd";
        brandi.hobbies = new string[] {"Art", "Business", "Cooking", "Using Instagram"};
        brandi.age = 19;
        brandi.height = 63;
    }
}
