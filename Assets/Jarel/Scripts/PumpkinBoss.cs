using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class PumpkinBoss : MonoBehaviour
{
    public PlayerStats stats;
    // make a refreance to the players posision 
    public Transform playerPos;
    public Transform seedLauncher;
    public float health = 8;
    bool isAttacking = false;
    public bool isDead = false;
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player
    float speed = 5.0f;
    // Start is called before the first frame update
    private Animator spriteAnimComp;

    public GameObject seed;
    public GameObject hoeDrop;

    public float speedOfSeed = 100f;
    private bool inRange = false;
    public bool pumpkinBossKill = false;

    EnemiesSpawner enemiesSpawner;

    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.Find("playerNormal").transform;
        stats = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
        enemiesSpawner = GameObject.Find("SpawnPoints").GetComponent<EnemiesSpawner>();
        spriteAnimComp = GetComponent<Animator>();

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

        if (health <= 0)
        {
            pumpkinBossKill = true;
            isDead = true;
            enemiesSpawner.pumpkinBossDead = true;
            spriteAnimComp.Play("pumpkinBoss_die");
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (!isAttacking && health > 0)
        {
            stats.health = stats.health - 1;
            isAttacking = true;
            speed = 0f;
            StartCoroutine(Wait());
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                // Trigger the attack behavior here when the player enters the trigger zone
                inRange = true;
                Debug.Log("Player detected, triggering attack behavior!");
                if (!isAttacking && health > 0)
                {
                    isAttacking = true;
                    speed = 0f;
                    spriteAnimComp.Play("pumpkinCrawler_attackTriple");
                    //StartCoroutine(Wait());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isDead)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                inRange = false;
            }
        }
    }

    void ContinueAttack()
    {
        if (inRange)
        {
            spriteAnimComp.Play("pumpkinCrawler_attackTriple");
        }
        else
        {
            isAttacking = false;
            speed = 5f;
            spriteAnimComp.Play("pumpkinCrawler_move");
        }
    }

    /*IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(1f);
        speed = 5f;
        isAttacking = false;
        spriteAnimComp.Play("pumpkinCrawler_move");
    }*/

    void CallDestroy()
    {
        enemiesSpawner.spawnCap += 25;
        StartCoroutine(DestroyEntity());
    }

    IEnumerator DestroyEntity()
    {
        Instantiate(hoeDrop, transform.position, transform.rotation);
        ScoreManager.instance.EnemyAddPoint(500);
        enemiesSpawner.bossThreshold = enemiesSpawner.scoreManager.score;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    public void PumpkinShoot()
    {
        StartCoroutine(SeedFlight());
    }

    IEnumerator SeedFlight()
    {
        GameObject pumpkinSeed = (GameObject)Instantiate(seed, seedLauncher.transform.position, transform.rotation);
        Vector3 directionToPlayer = (playerPos.transform.position - seedLauncher.transform.position).normalized;
        Vector3 lastPlayerPos = playerPos.transform.position;

        var speedVector = speedOfSeed * Time.deltaTime;
        while (pumpkinSeed != null)
        {
            pumpkinSeed.transform.position = Vector3.MoveTowards(pumpkinSeed.transform.position, lastPlayerPos, speedVector);
            yield return null;
        }
        Debug.Log("Terminate seed");
    }
}
