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
