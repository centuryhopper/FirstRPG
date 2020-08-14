using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    // this script is where the movement animation happens
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxPathLength = 40;
        NavMeshAgent agent;
        Animator anim;
        Health myHealth;
        ActionScheduler actionScheduler;

        // interface implementation
        public void Cancel()
        {
            agent.isStopped = true;
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            myHealth = GetComponent<Health>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.enabled = !myHealth.isDead;
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            // convert from global axis to the local axis
            Vector3 vel = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(vel);
            anim.SetFloat("forwardSpeed", localVelocity.z);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            // would cancels attack movement before doing regular movement
            actionScheduler.StartAction(this);
            MoveTo(destination, speedFraction);
        }

        // checks whether or not the player is close enough to move and attack the enemy
        public bool CanMoveTo(Vector3 enemyPosition)
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            bool hasPath =
            NavMesh.CalculatePath
            (transform.position, enemyPosition, NavMesh.AllAreas, navMeshPath);

            // is there a possible path to the destination
            if (!hasPath) { return false; }
            
            /// <summary>
            /// eliminates partial paths as well
            /// and only accepts COMPLETE paths
            /// </summary>
            if (navMeshPath.status != NavMeshPathStatus.PathComplete) { return false; }

            // if greater, than that means the destination is too far
            // than what we want, so return false
            if (GetPathLength(navMeshPath) > maxPathLength)
            {
                return false;
            }
            return true;
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

        // Do not call this method directly when transitioning the gameobject into movement mode
        // Only time you should call this method directly is if your moving because of attack mode
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            agent.isStopped = false;

            // speedFraction gives user the ability to tweak how fast a character moves
            // less than or equal to maxSpeed
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            agent.destination = destination;
        }

        [Serializable]
        private struct MoverSaveData
        {
            public SerializableVector3 position, rotation;
        }

        // must be serializable because this method is of type object
        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            // restore the state of the player from the last save
            // if this cast fails, then the compiler will throw an exception
            // SerializableVector3 prevPosition = (SerializableVector3) state;

            // disable navmesh agent to avoid glitches then re-enable after position
            // has been established
            if (agent != null)
                agent.enabled = false;

            transform.position = ((MoverSaveData) state).position.ToVector();
            transform.eulerAngles = ((MoverSaveData) state).rotation.ToVector();

            if (agent != null)
                agent.enabled = true;
        }
    }
}
