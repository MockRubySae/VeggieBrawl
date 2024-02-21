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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            GarlicGain(10);
        }
    }
    public void GarlicGain(float gain)
    {
        garlicMutationAmount += gain;
        garlicMutationAmount = Mathf.Clamp(garlicMutationAmount,0f,100f);
        garlicBar.fillAmount = garlicMutationAmount/100;

        playerStats.garlicMutation = garlicMutationAmount;
    }
}
