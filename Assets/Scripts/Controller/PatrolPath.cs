using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointIconRadius = .25f;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < transform.childCount;++i)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWayPointPosition(i), waypointIconRadius);
                Gizmos.DrawLine(GetWayPointPosition(i), GetWayPointPosition(j));
            }
        }

        public int GetNextIndex(int i)
        {
            return (i + 1) % transform.childCount;
        }

        public Vector3 GetWayPointPosition(int i)
        {
            return transform.GetChild(i).position;
        }

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
