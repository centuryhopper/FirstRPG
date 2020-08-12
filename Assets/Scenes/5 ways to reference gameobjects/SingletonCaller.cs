using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonCaller : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AddScore();
        }
    }

    void AddScore()
    {
        SingletonScoreManager.Instance.AddScore(1);
    }
}
