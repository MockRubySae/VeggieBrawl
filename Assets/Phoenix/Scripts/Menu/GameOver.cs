using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public PlayerStats playerStats;
    public GameObject inGameUi;

    // Update is called once per frame
    void Update()
    {
        if(playerStats.health <= 0)
        {
            gameOverScreen.SetActive(true);
            inGameUi.SetActive(false);
        }
        if(gameOverScreen.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
    }
    public void Restart(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}

