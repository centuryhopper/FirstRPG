using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using GameDevTV.Utils;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        GameObject player;
        NavMeshAgent agent;
        Animator animator;
        Fighter fighter;
        Health myHealth;
        Mover mover;
        ActionScheduler actionScheduler;
        float timeLastSeenPlayer = Mathf.Infinity, timeAtWayPoint = Mathf.Infinity;
        [SerializeField] float chaseDistance = 5f, suspicionTime = 2.5f, waypointDwellTime = 2.5f;
        [SerializeField] PatrolPath patrolPath;

        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = .2f;

        [SerializeField] float shoutDistance = 5f;
        int waypointIndex = 0;
        float tolerableDistance = 1f;

        // initial AI guarding position upon entering playmode
        LazyValue<Vector3> initialPos = null;

        public Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            fighter = GetComponent<Fighter>();
            myHealth = GetComponent<Health>();
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            initialPos = new LazyValue<Vector3>(GetGuardPosition);
        }

        // Start is called before the first frame update
        void Start()
        {
            initialPos.ForceInit();
        }

        // Update is called once per frame
        void Update()
        {
            if (myHealth.isDead) { return; }

            if (IsAggrevated() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            // will come into this condition upon player leaving the enemy chase radius
            else if (timeLastSeenPlayer < suspicionTime)
            {
                // guard gets suspicious so we cancel any action this enemy was going to perform
                // suspicion behavior
                SuspicionBehaviour();
            }
            else
            {
                fighter.Cancel();

                // return the guard to its original guarding spot

                // guard behavior (also the default behavior)
                PatrolBehaviour();
            }

            timeLastSeenPlayer += Time.deltaTime;
            aggrevated += Time.deltaTime;
            // timeAtWayPoint += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            // print(gameObject.name + " is chasing the player");
            timeLastSeenPlayer = 0;

            // Attack behaviour
            fighter.Attack(player);

            // in order to aggrevate enemy friends,
            // the enemy itself must be aggrevated already
            AggrevateNearbyEnemies();
        }

        private void AggrevateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
            // loops over all the hits
            // find any enemy components
            // aggrevate those enemies
            foreach (var hit in hits)
            {
                AIController ai = hit.collider.GetComponent<AIController>();
                if (ai == null) { continue; }
                ai.Aggrevate();
            }
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        [SerializeField] float aggrevatedDuration = 5f;
        float aggrevated = Mathf.Infinity;

        public void Aggrevate()
        {
            // sets the aggrevated timer
            aggrevated = 0;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = initialPos.value;
            if (patrolPath != null)
            {
                // if we're not at waypoint, then get there then dwell for a little bit
                if (AtWayPoint())
                {
                    if (timeAtWayPoint < waypointDwellTime)
                    {
                        timeAtWayPoint += Time.deltaTime;
                    }
                    else
                    {
                        // increment waypointIndex to get the waypoint
                        CycleWayPoint(ref waypointIndex);
                    }
                }
                else
                {
                    timeAtWayPoint = 0;
                }
                nextPosition = GetCurrentWayPoint(waypointIndex);
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private Vector3 GetCurrentWayPoint(int index)
        {
            return patrolPath.GetWayPointPosition(index);
        }

        private void CycleWayPoint(ref int index)
        {
            // virtually switch to the next waypoint index
            index = patrolPath.GetNextIndex(waypointIndex);
        }

        private bool AtWayPoint()
        {
            return Vector3.Distance(transform.position,patrolPath.GetWayPointPosition(waypointIndex)) < tolerableDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private bool IsAggrevated()
        {
            // has aggrevated timer expired yet
            // if its been a while, then don't be aggrevated anymore

            // might need to change logic to use mathf.infinity instead but we'll see where
            // testing takes us
            // if (Time.time - aggrevated > aggrevatedDuration)
            // {
            //     return false;
            // }


            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance
            || aggrevated < aggrevatedDuration;
        }
    }
}
