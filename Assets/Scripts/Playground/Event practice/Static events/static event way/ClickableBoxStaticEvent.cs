using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBoxStaticEvent : MonoBehaviour
{
    Collider collider;
    Camera camera;

    public static event Action<ClickableBoxStaticEvent> onAnyBoxClicked = delegate { };

    private void Awake()
    {
        collider = GetComponent<Collider>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            // the raycast can hit something that's inifinitely far away
            // because we passed Mathf.Infinity in

            // if it's Physics.Raycast, then clicking on any object would spawn particles
            // on the box, but because it's collider.Raycast, only clicking on the box will
            // cause particles
            if (collider.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                // call the action
                onAnyBoxClicked(this);
            }
        }
    }
}
