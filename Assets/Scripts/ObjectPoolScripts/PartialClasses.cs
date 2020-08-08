using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PartialClassPractice
{
    public class PartialClasses
    {
        [RuntimeInitializeOnLoadMethod]
        static void MainMethod()
        {
            // StarWarsClass sw = new StarWarsClass();
            // sw.Jedi();
            // sw.Sith();

            // try
            // {
            //     int x = 5;
            //     Debug.Log(x / 0);
            // }
            // catch (System.DivideByZeroException e)
            // {
            //     Debug.Log(e);
            //     // throw;
            // }
        }
    }

    public partial class StarWarsClass
    {
        public void Jedi()
        {
            Debug.Log("I'm a jedi");
        }
    }

    public partial class StarWarsClass
    {
        public void Sith()
        {
            Debug.Log("I'm a sith");
        }
    }

}
