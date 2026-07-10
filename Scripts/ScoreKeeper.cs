using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    float score = 0;

    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void Start()
    {
        ResetScore();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public float GetScore()
    {
        return score;
    }

    public void SetScore(float pointsToAdd)
    {
        score += pointsToAdd;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
