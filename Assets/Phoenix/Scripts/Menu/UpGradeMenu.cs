using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeMenu : MonoBehaviour
{
    public PlayerStats player;
    public GameObject upGrade;
    public GarlicFillAmount amount;
    public GameObject inGameUI;

    // Update is called once per frame
    void Update()
    {
        if (amount.garlicMutationAmount >= 100)
        {
            upGrade.SetActive(true);
            amount.Reset();
            inGameUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }
    public void AddAttackSpeed()
    {
        player.AttackSpeed++;
        inGameUI.SetActive(true);
        upGrade.SetActive(false);
    }
    public void AddStregth()
    {
        player.strength++;
        inGameUI.SetActive(true);
        upGrade.SetActive(false);
    }
    public void AddHealth()
    {
        player.maxHealth = player.maxHealth + 10;
        inGameUI.SetActive(true);
        upGrade.SetActive(false);
    }
}
