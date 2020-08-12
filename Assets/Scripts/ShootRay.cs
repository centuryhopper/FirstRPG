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
    
    void Start()
    {
        cam = GetComponent<Camera>();
    }
}
