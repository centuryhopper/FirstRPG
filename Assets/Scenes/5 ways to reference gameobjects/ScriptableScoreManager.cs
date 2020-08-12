using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableScoreManager", menuName = "Custom Score Manager", order = 0)]
public class ScriptableScoreManager : ScriptableObject
{
    [SerializeField] int score = 0;

    // if it weren't for this method, then the changes we make
    // in scene to 'score' would accumulate and never reset to 0
    private void OnEnable()
    {
        score = 0;
    }

    public void AddScore(int points)
    {
        score += points;
    }
}