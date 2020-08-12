using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adds and displays the score
public class ScoreManager0 : MonoBehaviour
{
    [SerializeField] int score = 0;

    public void AddScore(int points)
    {
        score += points;
    }
}
