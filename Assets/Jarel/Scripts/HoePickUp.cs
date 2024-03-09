using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoePickUp : MonoBehaviour
{
    public Movement player;
    public GameObject pressE;
    public PlayerStats playerStats;

    public Attack attack;
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player



    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();

        player = GameObject.Find("playerNormal").GetComponent<Movement>();
        attack = GameObject.Find("playerNormal").GetComponent<Attack>();
        pressE = gameObject.transform.GetChild(0).gameObject;
        playerStats = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // make the enemy look  at camera while standing
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            attack.hoeUiEnable.SetActive(true);
            attack.hoeUpgrade = true;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        pressE.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        pressE.SetActive(false);
    }
}
