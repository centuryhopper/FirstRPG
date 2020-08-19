using System;
using System.Linq;
using UnityEngine;

// predicate delegate takes only one input and returns a boolean
public class PredicateExample
{
    // [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        // Predicate<int> isOdd = IsOdd;
        // print(isOdd(5));

        // Predicate<string> isUpperCase = delegate (string word)
        // {
        //     return word.Equals(word.ToUpper());
        // };
        // print(isUpperCase("YO"));

        // print only even numbers in a list
        PrintBasedOnPredicate(new dynamic[] { 1,2,3,4,5,6,7,8,9,10 }, x => x % 2 == 0);
        // print('a' + 1);
    }

    static void PrintBasedOnPredicate(dynamic[] array, Predicate<dynamic> pred)
    {
        dynamic [] newArray = array.Where(n => pred(n)).ToArray();
        Array.ForEach(newArray, print);
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
