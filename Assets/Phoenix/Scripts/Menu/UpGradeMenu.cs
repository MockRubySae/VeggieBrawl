using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeMenu : MonoBehaviour
{
    public PlayerStats player;
    public GameObject upGrade;
    public GarlicFillAmount amount;
    public GameObject inGameUI;

    public GameObject playerHeadNormal;
    public GameObject playerBodyNormal;
    public GameObject playerRightArmNormal;
    public GameObject playerLeftArmNormal;
    public GameObject playerRightLegNormal;
    public GameObject playerLeftLegNormal;

    public GameObject playerHeadMutate;
    public GameObject playerBodyMutate;
    public GameObject playerRightArmMutate;
    public GameObject playerLeftArmMutate;
    public GameObject playerRightLegMutate;
    public GameObject playerLeftLegMutate;

    private int garlicUpCount = 0;
    private int pumpkinUpCount = 0;
    private int carrotUpCount = 0;

    public Animator playerAnimator;

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
        player.AttackSpeed += 0.5f;
        playerAnimator.SetFloat("speedBuff", player.AttackSpeed);
        carrotUpCount++;
        switch (carrotUpCount)
        {
            case 2:
                playerRightLegNormal.SetActive(false);
                playerRightLegMutate.SetActive(true);
                break;
            case 4:
                playerLeftLegNormal.SetActive(false);
                playerLeftLegMutate.SetActive(true);
                break;
            default:
                break;
        }
        inGameUI.SetActive(true);
        upGrade.SetActive(false);
    }
    public void AddStregth()
    {
        player.strength++;
        pumpkinUpCount++;
        switch (pumpkinUpCount)
        {
            case 2:
                playerRightArmNormal.SetActive(false);
                playerRightArmMutate.SetActive(true);
                break;
            case 4:
                playerLeftArmNormal.SetActive(false);
                playerLeftArmMutate.SetActive(true);
                break;
            default:
                break;
        }
        inGameUI.SetActive(true);
        upGrade.SetActive(false);
    }
    public void AddHealth()
    {
        player.maxHealth = player.maxHealth + 10;
        garlicUpCount++;
        switch (garlicUpCount)
        {
            case 2:
                playerBodyNormal.SetActive(false);
                playerBodyMutate.SetActive(true);
                break;
            case 4:
                playerHeadNormal.SetActive(false);
                playerHeadMutate.SetActive(true);
                break;
            default:
                break;
        }
        inGameUI.SetActive(true);
        upGrade.SetActive(false);
    }
}
