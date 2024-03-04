using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMover : MonoBehaviour
{
    public Transform playerPos;
    // Update is called once per frame
    void Update()
    {
        // move from current position to the position of the player
        transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, 10f);
    }
}
