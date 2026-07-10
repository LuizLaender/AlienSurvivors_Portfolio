using TMPro;
using UnityEngine;

public class HealthAndScoreUi : MonoBehaviour
{
    [SerializeField] GameObject[] healthPointsArray;
    [SerializeField] TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;
    private float currentHp;
    float startingHp;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        startingHp = GameObject.Find("Player").GetComponent<PlayerHealth>().GetPlayerHealth();
    }

    void Start()
    {
        currentHp = startingHp;
    }

    void Update()
    {
        for (int i = 0; i < healthPointsArray.Length; i++)
        {
            if (i < currentHp)
            {
                healthPointsArray[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                healthPointsArray[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }

    public void SetUiHP(float hp)
    {
        currentHp = hp;
    }
}
