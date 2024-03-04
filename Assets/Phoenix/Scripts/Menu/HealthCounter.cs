using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    public TextMeshProUGUI healthCount;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthCount.text = "Health: " + playerStats.health.ToString() + "/" + playerStats.maxHealth.ToString();
    }
}
