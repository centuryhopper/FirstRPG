using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigFunctions : MonoBehaviour
{
    [SerializeField] int sineAmplitude = 1, cosineAmplitude = 1, sineFrequency = 1, cosineFrequency = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =
        new Vector2(cosineAmplitude * Mathf.Cos(Time.time * sineFrequency), -sineAmplitude * Mathf.Sin(Time.time * cosineFrequency));
    }
}
