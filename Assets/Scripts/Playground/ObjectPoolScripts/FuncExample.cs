using System;
using UnityEngine;

// Func delegates can have 0 to 16 input parameters
// and can be used with an anonymous method or lambda expression
public class FuncExample
{
    // [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        // print("check");
        // print(SquareOrCube(3, 2));
        // print(SquareOrCube(3, 3));

        Func<float, int, float> squareOrCubeOgWay = SquareOrCube;
        print("og func assigment: " + squareOrCubeOgWay(3,2));

        Func<float, int, float> squareOrCubeDelegateWay = 
        delegate (float num, int power)
        {
            switch (power)
            {
                case 2:
                case 3:
                    return Mathf.Pow(num, power);
                default:
                    return 0;
            }   
        };
        print("delegate func assigment: " + squareOrCubeDelegateWay(3,3));

        Func<float, int, float> squareOrCubeLambdaWay =
        (num, power) => {
            switch (power)
            {
                case 2:
                case 3:
                    return Mathf.Pow(num, power);
                default:
                    return 0;
            }   
        };
        print("anonymous method (lambda expression) func assigment: " + squareOrCubeLambdaWay(3,4));


    }

    // take in a number and square it if power = 2 or cube it if power = 3, return 0 for all other numbers
    static float SquareOrCube(float num, int power)
    {
        switch (power)
        {
            case 2:
            case 3:
                return Mathf.Pow(num, power);
            default:
                return 0;
        }
    }

    static void print(dynamic msg)
    {
        Debug.Log(msg);
    }
}
