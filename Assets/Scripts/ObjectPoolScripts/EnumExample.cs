using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumExample : MonoBehaviour
{
    public enum BrandiMoods
    {
        Happy,
        Sad,
        Cute,
        Horny,
        Bitchy,
        Moody,
        Sarcastic,
        Annoyed,
        Angry,
        Loving,
        Hungry,
    }

    public BrandiMoods brandiMoods;

    // Start is called before the first frame update
    void Start()
    {
        brandiMoods = BrandiMoods.Happy;
    }

    // Update is called once per frame
    void Update()
    {
        switch(brandiMoods)
        {
            case BrandiMoods.Happy:
                Debug.Log("I love you leo :D");
                break;
            case BrandiMoods.Sad:
                Debug.Log("I is sad and need you, leo :'(");
                break;
            case BrandiMoods.Cute:
                Debug.Log("I feel cute today, leo");
                break;
            case BrandiMoods.Horny:
                Debug.Log("oohhh leo ;) I'm ready for my eggroll :P");
                break;
            case BrandiMoods.Bitchy:
                Debug.Log("leo, ur such a biznitch");
                break;
            case BrandiMoods.Moody:
                Debug.Log("I don't wanna call rn :(");
                break;
            case BrandiMoods.Sarcastic:
                Debug.Log("nah really?");
                break;
            case BrandiMoods.Annoyed:
                Debug.Log("*rolls eyes*");
                break;
            case BrandiMoods.Angry:
                Debug.Log("I don't like you, leo! : (");
                break;
            case BrandiMoods.Loving:
                Debug.Log("I wuv you bitch, I ain't never gonna stop loving you.....BITCH!!!");
                break;
            case BrandiMoods.Hungry:
                Debug.Log("Can I has some donuts? :P");
                break;
        }
    }
}
