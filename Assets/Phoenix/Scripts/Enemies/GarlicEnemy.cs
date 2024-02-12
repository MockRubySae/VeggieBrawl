using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GarlicEnemy : MonoBehaviour
{
    public PlayerStats stats;
    // make a refreance to the players posision 
    public Transform playerPos;
    
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player
    float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.Find("Player").transform;
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        // make the enemy look  at camera while standing
        transform.LookAt(Camera.main.transform, Vector3.up);
        // make varible to move the enemy times by deltatime so that it does not change with frame rate
        var step = speed * Time.deltaTime;
        // move from current position to the position of the player
        transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, step);
    }

}
