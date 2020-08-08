using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootRay : MonoBehaviour
{
    Ray lastRay;

    [HideInInspector] public static Camera cam;

    [SerializeField] int rayLength = 100;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        //     // lastRay = cam.ScreenPointToRay(Input.mousePosition);
        //     Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //     MovePlayerToMousePos(ray);
        // }
        // Debug.DrawRay(lastRay.origin, lastRay.direction * rayLength, Color.magenta);
    }

    // public void MoveTo(Vector3 destination)
    // {
    //     GameObject.Find("Player").GetComponent<NavMeshAgent>().destination = destination;
    // }

    // public void Stop()
    // {
    //     GameObject.Find("Player").GetComponent<NavMeshAgent>().isStopped = true;
    // }
}
