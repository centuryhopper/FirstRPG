using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        // this will be set to the parent of this gameobject
        [SerializeField] GameObject targetToDestroy = null;

        ParticleSystem ps;

        private void Start()
        {
            ps = GetComponent<ParticleSystem>();
        }


        // Update is called once per frame
        void Update()
        {
            if (!ps.IsAlive())
            {
                // if this gameobject has a parent
                if (targetToDestroy != null)
                {
                    print("destroying parent");
                    Destroy(targetToDestroy);
                }
                else
                {
                    // if our level up particle producting gameobject
                    // isn't childed to a parent, then we go in here
                    print("destroying itself");
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
