using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AsyncAwait
{

    // without async, new thread should print first
    // with async, main thread gets printed first
    // while the LongTask() does its work in the
    // background
    [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        // Method();
        // print("Main thread");
    }

    private static async void Method()
    {
        await Task.Run(new Action(LongTask));
        print("New Thread");
    }

    private static void LongTask()
    {
        Thread.Sleep(5000);
    }

    static void print(dynamic message)
    {
        Debug.Log(message);
    }
}
