using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Oscillator : MonoBehaviour
{
    public enum TrigTool
    {
        Sine, Cosine, SineAndCosine, Tangent
    }
    [SerializeField] float amplitude = 10;

    [Tooltip("please specify a trig function to use")]
    [Label("Trig function")]
    [SerializeField] TrigTool trigTool;

    void Start()
    {
        // global position
        transform.position = new Vector3(0,0,100);

        // position relative to its parent. If parentless, then identical to global position
        // transform.localPosition = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        float x = amplitude * Mathf.Cos(Time.timeSinceLevelLoad);
        float y = amplitude * Mathf.Sin(Time.timeSinceLevelLoad);
        switch (trigTool)
        {
            case TrigTool.Sine:
                transform.position = 
                new Vector3(transform.position.x, y, transform.position.z);
                break;
            case TrigTool.Cosine:
                transform.position = 
                new Vector3(x, transform.position.y, transform.position.z);
                break;
            case TrigTool.SineAndCosine:
                transform.position = 
                new Vector3(x, y, transform.position.z);
                break;
            case TrigTool.Tangent:
                transform.position = 
                new Vector3(transform.position.y, transform.position.y, amplitude * Mathf.Tan(Time.timeSinceLevelLoad));
                break;
        }

        
    }
}
