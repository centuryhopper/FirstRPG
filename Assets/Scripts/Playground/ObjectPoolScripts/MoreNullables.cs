using System;
using UnityEngine;


public class MoreNullables
{
    [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        // int? datatype is the same as Nullable<int>
        int? a = null;
        Nullable<int> b = 85;

        int c = a ?? 0;
        int d = b ?? 10;

        // Debug.Log(a);
        // Debug.Log(b);
        // Debug.Log(c);
        // Debug.Log(d);   
    }

}
