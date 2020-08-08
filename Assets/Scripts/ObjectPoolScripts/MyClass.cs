// Create a non-MonoBehaviour class which displays
// messages when a game is loaded.
using UnityEngine;

/// <summary>
/// at least one method must be static
/// </summary>

class MyClass
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        // Debug.Log("After Scene is loaded and game is running");

        // if we mark as nonstatic
        // MyClass mc = new MyClass();
        // mc.OnSecondRuntimeMethodLoad();
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnSecondRuntimeMethodLoad()
    {
        // Debug.Log("SecondMethod After Scene is loaded and game is running.");
    }
}
