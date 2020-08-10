using System;
using UnityEngine;

// predicate delegate takes only one input and returns a boolean
public class PredicateExample
{
    [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        Predicate<int> isOdd = IsOdd;
        print(isOdd(5));

        Predicate<string> isUpperCase = delegate (string word)
        {
            return word.Equals(word.ToUpper());
        };
        print(isUpperCase("YO"));
    }

    static bool IsOdd(int num)
    {
        return num % 2 == 1;
    }

    static bool IsUpperCase(string word)
    {
        return word.Equals(word.ToUpper());
    }

    static void print(dynamic msg)
    {
        Debug.Log(msg);
    }
}
