using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SceneParentLife : MonoBehaviour
    {
        // [HideInInspector]
        // public bool toToggleInPortalClass = false;

        private void OnEnable()
        {
            // subsribe event
            Portal.DestroyParentGameObj += DestroyObject;
        }

        private void OnDestroy()
        {
            // unsubsribe event
            Portal.DestroyParentGameObj -= DestroyObject;
        }

        void DestroyObject()
        {
            Destroy(this.gameObject);
        }

        // Update is called once per frame
        // void Update()
        // {
        //     if (toToggleInPortalClass)
        //     {
        //         Destroy(gameObject);
        //     }
        // }
    }    
}
