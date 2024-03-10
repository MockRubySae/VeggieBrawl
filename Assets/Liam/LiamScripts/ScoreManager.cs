using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    public int score = 0;
    int highscore = 0;

    private float nextScoreTime = 0.0f;
    public float period = 1f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();
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

        if(highscore < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void EnemyAddPoint(int points)
    {
        score += points;
    }
    public void ScoreReset()
    {
        score = 0;
    }
}
