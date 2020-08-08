using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParentLife : MonoBehaviour
{
    [HideInInspector]
    public bool toToggleInPortalClass = false;

    // Update is called once per frame
    void Update()
    {
        if (toToggleInPortalClass)
        {
            Destroy(gameObject);
        }
    }
}
