using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Resources;

namespace RPG.Combat
{
    // makes sure that the health script must be attached
    // to the same gameobject this current script is attached to
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
