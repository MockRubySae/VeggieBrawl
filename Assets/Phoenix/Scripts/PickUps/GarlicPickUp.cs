using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicPickUp : MonoBehaviour
{
    public GarlicFillAmount garlic;
    public Movement player;
    public GameObject pressE;
    public PlayerStats playerStats;
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player

    

    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();

        garlic = GameObject.Find("Canvas").GetComponent<GarlicFillAmount>();
        player = GameObject.Find("Player").GetComponent<Movement>();
        pressE = gameObject.transform.GetChild(0).gameObject;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // make the enemy look  at camera while standing
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            StartCoroutine(Eating());
            Debug.Log("isEaing");
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
    IEnumerator Eating()
    {
        Debug.Log("starting to eat");
        player.speed = 0f;
        yield return new WaitForSeconds(1);
        garlic.GarlicGain(25);
        player.speed = 10f;
        playerStats.health = playerStats.health + 10;
        Destroy(gameObject);
        Debug.Log("finished eatingt");
    }

}
