using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarlicFillAmount : MonoBehaviour
{
    public float garlicMutationAmount;
    public Image garlicBar;
    public PlayerStats playerStats;
    int timesUpGarded;
    // Start is called before the first frame update
    public void Start()
    {
        timesUpGarded = 1;
    }
    public void GarlicGain(float gain)
    {
        garlicMutationAmount += gain/timesUpGarded;
        garlicMutationAmount = Mathf.Clamp(garlicMutationAmount,0f,100f);
        garlicBar.fillAmount = garlicMutationAmount/100f;

        playerStats.garlicMutation = garlicMutationAmount;
    }
    public void Reset()
    {
        garlicMutationAmount = 0f;
        garlicMutationAmount = Mathf.Clamp(garlicMutationAmount, 0f, 100f);
        garlicBar.fillAmount = garlicMutationAmount / 100;
        timesUpGarded++;
    }
}
