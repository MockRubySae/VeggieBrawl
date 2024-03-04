using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public float maxHealth = 100f;
    public Movement movementSpeed;
    public float strength = 1.0f;
    public float AttackSpeed = 1.0f;
    public float luck = 1.0f;
    public float garlicMutation = 0.0f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
    }
}
