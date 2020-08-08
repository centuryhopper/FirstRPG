using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColor : MonoBehaviour
{
    void ChangeColor(Color colour)
    {
        GetComponent<MeshRenderer>().material.color = colour;
    }

    // Start is called before the first frame update
    void Start()
    {
        // subscribe the event
        ColorController.colorEventHandler += ChangeColor;   
    }

    private void OnDisable()
    {
        // let colorEventHandler unsubscribe from the ChangeColor method for this particular
        // gameobject bc it's being destroyed/disabled
        ColorController.colorEventHandler -= ChangeColor;
    }
}
