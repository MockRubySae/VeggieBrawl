using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PumpkinEnemy : MonoBehaviour
{
    public PlayerStats stats;
    // make a refreance to the players posision 
    public Transform playerPos;
    public float health = 4;
    bool isAttacking = false;
    // rigidbody refreance
    Rigidbody pumpkinRb;
    // make speed half of player
    float speed = 10.0f;
    // Start is called before the first frame update

    private Animator spriteAnimComp;

    void Start()
    {
        // give rigid body
        pumpkinRb = GetComponent<Rigidbody>();
        playerPos = GameObject.Find("Player").transform;
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();

        spriteAnimComp = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // make the enemy look  at camera while standing
        transform.LookAt(Camera.main.transform, Vector3.up);
        // make varible to move the enemy times by deltatime so that it does not change with frame rate
        var crawl = speed * Time.deltaTime;
        // move from current position to the position of the player
        transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, crawl);
        Debug.Log(crawl);
        if (health <= 0)
        {
            spriteAnimComp.Play("pumpkinCrawler_death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAttacking)
        {
            stats.health = stats.health - 1;
            isAttacking = true;
            speed = 0f;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        spriteAnimComp.Play("pumpkinCrawler_attack");
        yield return new WaitForSeconds(1f);
        speed = 5f;
        isAttacking = false;
        spriteAnimComp.Play("pumpkinCrawler_move");
    }

    void CallDestroy()
    {
        StartCoroutine(DestroyEntity());
    }

    IEnumerator DestroyEntity()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
