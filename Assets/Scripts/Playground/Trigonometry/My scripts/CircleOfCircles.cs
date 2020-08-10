using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrigStuff;

public class CircleOfCircles
{

    // [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        float radius = 100;
        int cnt = 0, height = 1000;
        MakeDoubleHelix(radius, ref cnt, height);
        

        print("there are " + cnt + " spheres spawned");
    }

    static void MakeCircle(float radius, ref int cnt)
    {
        // because 2 * pi is a full circle
        for (float radians = 0; radians < 2 * Mathf.PI; radians += 0.1f)
        {
            float x = radius * Mathf.Cos(radians)
            ,y = radius * Mathf.Sin(radians)
            ,z = 5;

            TrigTools.MakeSphere(x,y,z, TrigTools.GetRandomColor(), 5);
            ++cnt;
            print("radians: " + radians);
        }
    }

    static void MakeDoubleHelix(float radius, ref int cnt, int height)
    {
        float y = 0;
        // because 2 * pi is a full circle
        for (float radians = 0; radians < 2 * Mathf.PI * height; radians += 0.1f)
        {
            float x = radius * Mathf.Cos(radians)

            // swapped the y and z values
            ,z = radius * Mathf.Sin(radians);
            y += 5;

            TrigTools.MakeSphere(x,y,z, TrigTools.GetRandomColor(), 5);

            x = radius * Mathf.Cos(radians + Mathf.PI);
            z = radius * Mathf.Sin(radians + Mathf.PI);
            TrigTools.MakeSphere(x,y,z, TrigTools.GetRandomColor(), 5);
            ++cnt;
            print("radians: " + radians);
        }
    }

    static void print(dynamic msg)
    {
        Debug.Log(msg);
    }
}
