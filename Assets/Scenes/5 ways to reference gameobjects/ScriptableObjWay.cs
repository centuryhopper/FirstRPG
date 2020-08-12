using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjWay : MonoBehaviour
{
    [SerializeField] ScriptableScoreManager scoreManager = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AddScore();
        }
    }

    void AddScore()
    {
        scoreManager.AddScore(1);
    }
}
