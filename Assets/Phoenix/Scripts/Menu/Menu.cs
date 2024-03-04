using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menuUi;
    public GameObject inGameUi;
    // Update is called once per frame
    void Update()
    {
        if (menuUi.activeInHierarchy == true)
        {
            Time.timeScale = 0f;
            inGameUi.SetActive(false);
        }
        else if (inGameUi.activeInHierarchy == true)
        {
            Time.timeScale = 1f;
        }
    }
    public void Play()
    {
        menuUi.SetActive(false);
        Time.timeScale = 1f;
        inGameUi.SetActive(true);
        ScoreManager.instance.ScoreReset();
    }
}
