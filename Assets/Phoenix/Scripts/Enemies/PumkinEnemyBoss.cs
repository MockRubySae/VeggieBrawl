using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class PumkinEnemyBoss : MonoBehaviour
{
    public PlayerStats stats;
    // make a refreance to the players posision 
    public Transform playerPos;
    public float health = 10;
    bool isAttacking = false;
    public bool isDead = false;
    public bool standing = false;
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player
    float speed = 5.0f;
    // Start is called before the first frame update
    private Animator spriteAnimComp;

    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.Find("Player").transform;
        stats = GameObject.Find("Player").GetComponent<PlayerStats>();
        //   spriteAnimComp = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // make the enemy look  at camera while standing
            transform.LookAt(Camera.main.transform, Vector3.up);
            // make varible to move the enemy times by deltatime so that it does not change with frame rate
            var step = speed * Time.deltaTime;
            // move from current position to the position of the player
            transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, step);
        }
       
        if(health <= 0 && !isDead)
        {
            isDead = true;
        //    spriteAnimComp.Play("garlicSpider_death");
            StartCoroutine(DestroyEntity());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isAttacking && health > 0)
        {
            isAttacking = true;
            speed = 0f;
            StartCoroutine(Wait());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        speed = 5f;
    }
    IEnumerator Wait()
    {
    //    spriteAnimComp.Play("garlicSpider_attack");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
     //   spriteAnimComp.Play("garlicSpider_move");
    }

    void CallDestroy()
    {
        StartCoroutine(DestroyEntity());
    }

    IEnumerator DestroyEntity()
    {
     //   int changeToDrop = Random.Range(0, 10);
     //   if (changeToDrop >= 8)
     //   {
    //        Instantiate(garlicDrop, transform.position, transform.rotation);
     //   }
        ScoreManager.instance.EnemyAddPoint(1250);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
