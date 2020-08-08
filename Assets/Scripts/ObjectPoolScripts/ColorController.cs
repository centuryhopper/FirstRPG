using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{

    [SerializeField] Color myColor;
    public delegate void colorController(Color color);

    // will act as the temporary function container for CustomColor.ChangeColor()
    public static event colorController colorEventHandler;

    #region Singleton
    public static ColorController instance;
    private static ColorController _instance;
    #endregion

    private void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // check for subsription before executing code
            if (colorEventHandler != null)
            {
                colorEventHandler(myColor);
            }
        }
    }
}
