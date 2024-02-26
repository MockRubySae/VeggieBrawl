using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI highscoreText;

    int score = 0;
    //int highscore = 0;

    private float nextScoreTime = 0.0f;
    public float period = 1f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        if (Time.time > nextScoreTime)
        {
            nextScoreTime += period;
            if (Time.time < 60)
            {
                score += 10;
            }
            else if (Time.time > 60 && Time.time < 120)
            {
                score += 30;
            }
            else if (Time.time > 120 && Time.time < 180)
            {
                score += 50;
            }
            else if (Time.time > 180 && Time.time < 240)
            {
                score += 70;
            }
            else if (Time.time > 300)
            {
                score += 100;
            }
        }
    }

    public void EnemyAddPoint()
    {
        score += 100;
    }
    public void ScoreReset()
    {
        score = 0;
    }
}
