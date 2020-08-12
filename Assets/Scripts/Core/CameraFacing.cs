using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        // get the camera and then always look at it
        Camera cam;

        void Start()
        {
            // cache camera.main to a pointer
            // so that we don't make this expensive call
            // all the time
            cam = Camera.main;
        }

        void Update()
        {
            // always look at camera
            // transform.LookAt(cam.transform);
            // OR...
            transform.forward = cam.transform.forward;
        }
    }
}
