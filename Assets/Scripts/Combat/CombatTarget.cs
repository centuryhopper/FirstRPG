using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Resources;
using RPG.Control;

namespace RPG.Combat
{
    // makes sure that the health script must be attached
    // to the same gameobject this current script is attached to
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            // if the enemy is gone, then we shouldn't try to move to it
            // if the enemy cant be attacked
            if (!callingController.GetComponent<Fighter>().CanAttack(this.gameObject))
            {
                return false;
            }
            // onclick
            if (Input.GetMouseButton(0))
            {
                transform.LookAt(this.transform);
                callingController.GetComponent<Fighter>().Attack(this.gameObject);
            }

            return true;
        }
    }
}
