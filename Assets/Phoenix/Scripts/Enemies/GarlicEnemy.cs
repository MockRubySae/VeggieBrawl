using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class GarlicEnemy : MonoBehaviour
{
    public PlayerStats stats;
    // make a refreance to the players posision 
    public Transform playerPos;
    public float health = 2;
    bool isAttacking = false;
    public bool isDead = false;
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player
    float speed = 5.0f;
    // Start is called before the first frame update
    public GameObject garlicDrop;
    private Animator spriteAnimComp;

    public EnemiesSpawner spawnCounter;

    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.Find("playerNormal").transform;
        stats = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
        spawnCounter = GameObject.Find("SpawnPoints").GetComponent<EnemiesSpawner>();
        spriteAnimComp = GetComponent<Animator>();
        spawnCounter.spawnCount++;

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
       
        if(health <= 0)
        {
            isDead = true;
            spriteAnimComp.Play("garlicSpider_death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAttacking && health > 0)
        {
            stats.health = stats.health - 1;
            isAttacking = true;
            speed = 0f;
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        spriteAnimComp.Play("garlicSpider_attack");
        yield return new WaitForSeconds(1f);
        speed = 5f;
        isAttacking = false;
        spriteAnimComp.Play("garlicSpider_move");
    }

    void CallDestroy()
    {
        spawnCounter.spawnCount--;
        StartCoroutine(DestroyEntity());
    }

    IEnumerator DestroyEntity()
    {
        int changeToDrop = Random.Range(0, 10);
        if (changeToDrop >= 8)
        {
            Instantiate(garlicDrop, transform.position, transform.rotation);
        }
        ScoreManager.instance.EnemyAddPoint(100);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
