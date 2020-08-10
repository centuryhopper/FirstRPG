using UnityEngine;
using System;
public class Indexers
{
    [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        SampleCollection sc = new SampleCollection();
        sc[9] = 50;
        // Array.Fill(sc, 0);
        for (int i = 0; i < 10; ++i)
        {
            // Debug.Log(sc[i]);
        }

        // Debug.Log(sc[10]);
    }

}

public class SampleCollection
{
    private int[] array = new int[10];

    public int this[int indx]
    {
        get { return array[indx]; }
        set { array[indx] = value; }
    }
}

