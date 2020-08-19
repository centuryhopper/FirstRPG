using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class AsyncProgramming
{
    // make three tasks
    // let task 2 depend on task one
    // let task 3 run independently

    // [RuntimeInitializeOnLoadMethod]
    static void MainMethod()
    {
        Compute();
        Task3();
    }

    static void Task3()
    {
        print("task3!");
    }

    async static void Compute()
    {
        int task1Res = await Task.Run(() => Task1());

        print(Task2(task1Res));
    }

    static int Task1()
    {
        Thread.Sleep(500);
        return 100;
    }

    static int Task2(int res1)
    {
        return res1 + 100;
    }

    static void print(object msg)
    {
        Debug.Log(msg);
    }
}
