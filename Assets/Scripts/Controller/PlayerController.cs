﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Movement;
using RPG.Combat;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        CombatTarget combatTarget;
        Health myHealth;
        
        [Serializable]
        struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D texture;
            public Vector2 hotSpot;
        }
        
        // should include an element for movement, combat, and none
        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxPathLength = 40;

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotSpot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            // this array should always be of size one
            return cursorMappings.Where(x => x.cursorType == type).ToArray()[0];
        }

        private void Awake()
        {
            myHealth = GetComponent<Health>();
        }

        void Update()
        {
            if (InteractingWithUI()) { SetCursor(CursorType.UI); return; }
            if (myHealth.isDead) { SetCursor(CursorType.None); return; }

            if (InteractWithComponent()) { return; }
            if (MovementInteraction()) { return; }

            // print("clicked out of world bounds or idling");
            SetCursor(CursorType.None);
        }

        // generic raycasting
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                // get all of its IRaycastable components
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private RaycastHit[] RaycastAllSorted()
        {
            // Get all hits
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];

            // Sort by distance
            // build array distances
            for (int i = 0; i < distances.Length; ++i)
            {
                distances[i] = hits[i].distance;
            }

            // Sort the hits
            Array.Sort(distances, hits);

            // Return

            return hits;
        }

        private bool InteractingWithUI()
        {
            // Check if the mouse was clicked over a UI element
            return EventSystem.current.IsPointerOverGameObject();
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

        private bool MovePlayerToMousePos()
        {            
            // target will be the position of the hit point on the navmesh
            Vector3 target;
            // pass target by reference (bc it is an out param, it MUST be modified inside the method)
            bool canHit = RaycastNavMesh(out target);
            if (canHit)
            {
                if (Input.GetMouseButton(0))
                {
                    // cancel fighting and start regular movement action
                    // GetComponent<Fighter>().Cancel();
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }

            // if can't hit, then don't do anything
           return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = Vector3.zero;
            RaycastHit hit;
            bool hasHitSomething = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHitSomething) { return false; }

            // raycast to the terrain
            NavMeshHit navMeshHit;
            // use the hit.point from earlier as the source to finding the
            // nearest navmesh point
            bool hasNearestPoint = 
            // change infinity to a configuration parameter later
            NavMesh.SamplePosition(hit.point, out navMeshHit, Mathf.Infinity, NavMesh.AllAreas);
            if (hasNearestPoint)
            {
                target = navMeshHit.position;
                NavMeshPath navMeshPath = new NavMeshPath();
                bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, navMeshPath);
                if (!hasPath) { return false; }
                
                /// <summary>
                /// eliminates partial paths as well
                /// and only accepts COMPLETE paths
                /// </summary>
                if (navMeshPath.status != NavMeshPathStatus.PathComplete)
                {
                    return false;
                }

                // if greater, than that means the destination is too far
                // than what we want, so return false
                if (GetPathLength(navMeshPath) > maxPathLength)
                {
                    return false;
                }

                return true;
            }

            // if we the area is not clickable
            return false;
        }

        private float GetPathLength(NavMeshPath navMeshPath)
        {
            // sum up the distance between each corner position from the path
            // to get the total path length
            Vector3[] path = navMeshPath.corners;
            float tot = 0;
            // stop at the second to last element, so
            // we can avoid an array indexoutofbounds error
            for (int i = 0; i < path.Length - 1; ++i)
            {
                tot += Vector3.Distance(path[i], path[i + 1]);
            }

            return tot;
        }
    }
}
