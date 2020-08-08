using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        CombatTarget combatTarget;

        Health myHealth;

        private void Awake()
        {
            myHealth = GetComponent<Health>();
        }

        // gives the developer more freedom over using the ray shooting out of the camera
        private static Ray GetMouseRay()
        {
            return ShootRay.cam.ScreenPointToRay(Input.mousePosition);
        }

        // wrapper methods
        private bool MovementInteraction()
        {
            return MovePlayerToMousePos();
        }

        private bool CombatInteraction()
        {
            return MovePlayerToEnemy();
        }

        private bool MovePlayerToEnemy()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                // if we hit an enemy (all enemies should have a CombatTarget.cs component!)
                combatTarget = hit.transform.GetComponent<CombatTarget>();

                if (combatTarget != null)
                {
                    print("combat target: " + combatTarget.gameObject.name);
                    // if the enemy is gone, then we shouldn't try to move to it
                    if (!GetComponent<Fighter>().CanAttack(combatTarget.gameObject))
                    {
                        continue;
                    }
                    if (Input.GetMouseButton(0))
                    {
                        transform.LookAt(combatTarget.transform);
                        GetComponent<Fighter>().Attack(combatTarget.gameObject);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool MovePlayerToMousePos()
        {
            RaycastHit hitSomething;
            bool hasHit = false;
            if (Input.GetMouseButton(0))
            {
                hasHit = Physics.Raycast(GetMouseRay(), out hitSomething);
                if (hasHit)
                {
                    // cancel fighting and start regular movement action
                    GetComponent<Fighter>().Cancel();
                    GetComponent<Mover>().StartMoveAction(hitSomething.point, 1f);
                }
            }
            // Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.magenta);
            return hasHit;
        }

        // Update is called once per frame
        void Update()
        {
            if (myHealth.isDead) {return;}
            
            if (CombatInteraction())
            {
                // Debug.Log("attacking");
                return;
            }
            if (MovementInteraction())
            {
                // Debug.Log("moving");
                return;
            }
            // Debug.Log("clicked out of world bounds or idling");
        }
    }
}
