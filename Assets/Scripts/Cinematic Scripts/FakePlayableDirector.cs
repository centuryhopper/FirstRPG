using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Cinematics
{
    public class FakePlayableDirector : MonoBehaviour
    {
        public event Action<float> onFinish;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("OnFinish", 3f);
        }

        void OnFinish()
        {
            // action gets called here
            // (given that the methods disablecontrol and enablecontrol have subscribed to it)
            onFinish(2f);
        }
    }
}

