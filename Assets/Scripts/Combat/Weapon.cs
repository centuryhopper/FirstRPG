using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] UnityEvent onHit = null;

        // will be called from the fighter script
        // to invoke a weapon sound
        public void OnHit()
        {
            onHit.Invoke();
        }
    }
}
