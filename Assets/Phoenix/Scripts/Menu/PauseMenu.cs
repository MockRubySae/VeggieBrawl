using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Movement player;
    public GameObject inGameUi;
    public GameObject pause;
    public GameObject tic;
    bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            inGameUi.SetActive(false);
            pause.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            pause.SetActive(false);
            isPaused = false;
            inGameUi.SetActive(true);
        }
    }
    public void Resume()
    {
        pause.SetActive(false);
        isPaused = false;
        inGameUi.SetActive(true);
    }
    public void ToggleShift()
    {
        if(player.isToggleSprint == false)
       {
            player.isToggleSprint = true;
            tic.SetActive(true);
       }
        else if (player.isToggleSprint == true)
        {
            player.isToggleSprint = false;
            tic.SetActive(false);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
