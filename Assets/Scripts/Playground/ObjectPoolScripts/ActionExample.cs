using System;
using UnityEngine;

// Action delegates are func delegates but have a void return type
// Action delegates can have 0 to 16 input parameters
// and can be used with an anonymous method or lambda expression
public class ActionExample
{
    // [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        Action<float, int> squareOrCubeOgWay = SquareOrCube;
        squareOrCubeOgWay(3,2);

        Action<float, int> squareOrCubeDelegateWay =
        delegate (float num, int power)
        {
            switch (power)
            {
                case 2:
                case 3:
                    print("action delegate way: " + Mathf.Pow(num, power));
                    break;
                default:
                    print(0);
                    break;
            }
        };
        squareOrCubeDelegateWay(5,2);

        Action<float, int> squareOrCubeLambdaWay =
        (num, power) => {
            switch (power)
            {
                case 2:
                case 3:
                    print("action lambda way: " + Mathf.Pow(num, power));
                    break;
                default:
                    print(0);
                    break;
            }   
        };
        squareOrCubeLambdaWay(5,3);
    }

    // take in a number and square it if power = 2 or cube it if power = 3, return 0 for all other numbers
    static void SquareOrCube(float num, int power)
    {
        switch (power)
        {
            case 2:
            case 3:
                print("og action: " + Mathf.Pow(num, power));
                break;
            default:
                print(0);
                break;
        }
    }

    static void print(dynamic msg)
    {
        Debug.Log(msg);
    }
}
