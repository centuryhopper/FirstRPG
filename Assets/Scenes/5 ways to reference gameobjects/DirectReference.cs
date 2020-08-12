using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responds to clicks
// Dependency injection is used if scoreManager0 is null
public class DirectReference : MonoBehaviour
{
    [SerializeField] ScoreManager0 scoreManager0 = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AddScore();
        }
    }

    public void Initialize(ScoreManager0 injectedScoreManager)
    {
        if (scoreManager0 == null)
        {
            scoreManager0 = injectedScoreManager;
        }   
    }

    void AddScore()
    {
        scoreManager0.AddScore(1);
    }
}
