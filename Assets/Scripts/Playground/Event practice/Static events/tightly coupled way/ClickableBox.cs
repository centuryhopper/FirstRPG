using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBox : MonoBehaviour
{
    Collider collider;
    ParticleController particleController;
    Camera camera;

    private void Awake()
    {
        collider = GetComponent<Collider>();
        
        // we could optionally also make this a singleton
        particleController = FindObjectOfType<ParticleController>();
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
            if (collider.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                // particleController.SpawnParticleOnBox(this);
            }
        }
    }
}
