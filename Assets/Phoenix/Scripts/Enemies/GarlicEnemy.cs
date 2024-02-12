using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GarlicEnemy : MonoBehaviour
{
    // make a refreance to the players posision 
    public GameObject playerPos;
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player
    float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // make a varible for where to look
        Vector3 lookpos = playerPos.transform.position;
        // make the enemy look  at player while standing
        transform.LookAt(lookpos, Vector3.up);
        // make varible to move the enemy times by deltatime so that it does not change with frame rate
        var step = speed * Time.deltaTime;
        // move from current position to the position of the player
        transform.position = Vector3.MoveTowards(transform.position, lookpos, step);
    }
}
