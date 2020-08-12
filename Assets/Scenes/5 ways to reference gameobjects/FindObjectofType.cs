using UnityEngine;

public class FindObjectofType : MonoBehaviour
{

    ScoreManager0 scoreManager0;

    void Awake()
    {
        scoreManager0 = FindObjectOfType<ScoreManager0>();
    }

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
        scoreManager0.AddScore(1);
    }
}