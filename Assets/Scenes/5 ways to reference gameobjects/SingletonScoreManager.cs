using UnityEngine;

public class SingletonScoreManager : MonoBehaviour
{
    [SerializeField] int score = 0;

    #region Singleton creation

    // static so that it is shared between scenes
    public static SingletonScoreManager Instance {get; private set;} = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // we come in here if there's more than one instance of this component
            // typically would happen if we transition to another scene
            Destroy(this.gameObject);
        }
    }

    #endregion

    public void AddScore(int points)
    {
        score += points;
    }
}