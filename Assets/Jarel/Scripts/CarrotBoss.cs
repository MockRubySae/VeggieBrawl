using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class CarrotBoss : MonoBehaviour
{
    PlayerStats stats;
    // make a refreance to the players posision 
    Transform playerPos;
    Transform terrain;

    public float health = 10;
    bool isAttacking = false;
    public bool isDead = false;
    
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player
    float speed = 3.0f;
    float speedOfSpike = 50f;

    public GameObject bossSpike;
    public GameObject spikeSpawner;
    public GameObject spearDrop;

    Animator spriteAnimComp;
    EnemiesSpawner enemiesSpawner;

    private bool inRange = false;

    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();
        terrain = GameObject.Find("Ground").transform;
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
            speed = 0f;
            isDead = true;
            enemiesSpawner.carrotBossDead = true;
            spriteAnimComp.Play("carrotBoss_die");
        }
    }

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
                    spriteAnimComp.Play("carrotSquid_bossAttack");
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

    void CallAttack() //keyframe event
    {
        //not the actual attack object; this just creates a line towards the player for the spike objects to spawn in
        /*GameObject carrotSpikeLauncher = (GameObject)Instantiate(spikeSpawner, transform.position, transform.rotation);
        Vector3 directionToPlayer = (playerPos.transform.position - transform.position).normalized;
        carrotSpikeLauncher.GetComponent<Rigidbody>().AddForce(carrotSpikeLauncher.transform.forward * speedOfSpike);*/
        StartCoroutine(SeedFlight()); //more accurate than AddForce
    }

    IEnumerator SeedFlight()
    {
        GameObject carrotSpikeLauncher = (GameObject)Instantiate(spikeSpawner, transform.position, terrain.transform.rotation);
        Vector3 directionToPlayer = (playerPos.transform.position - transform.position).normalized;

        //so the projectil doesnt stop at the last position of the player and overshoots
        Vector3 lastPlayerPos = new Vector3(playerPos.transform.position.x, terrain.transform.position.y, playerPos.transform.position.z);
        Vector3 extendedTargetPos = lastPlayerPos + (lastPlayerPos - transform.position).normalized * 50f;

        var speedVector = speedOfSpike * Time.deltaTime;
        while (carrotSpikeLauncher != null)
        {
            carrotSpikeLauncher.transform.position = Vector3.MoveTowards(carrotSpikeLauncher.transform.position, extendedTargetPos, speedVector);
            yield return null;
        }
        Debug.Log("Terminate seed");
    }

    void Cooldown()
    {
        StartCoroutine(CooldownBehavior());
    }

    IEnumerator CooldownBehavior()
    {
        if (inRange)
        {
            spriteAnimComp.Play("carrotSquid_idle");
        }
        else
        {
            isAttacking = false;
            speed = 3f;
            spriteAnimComp.Play("carrotSquid_move");
        }
        yield return new WaitForSeconds(3f);
        ContinueAttack();
    }

    void ContinueAttack() //keyframe event
    {
        if (inRange)
        {
            speed = 0f;
            isAttacking = true;
            Debug.Log("Reattacking");
            spriteAnimComp.Play("carrotSquid_bossAttack");
        }
        else
        {
            isAttacking = false;
            speed = 3f;
            spriteAnimComp.Play("carrotSquid_move");
        }
    }

    void CallDestroy() //keyframe event
    {
        enemiesSpawner.spawnCap += 25;
        StartCoroutine(DestroyEntity());
    }

    IEnumerator DestroyEntity()
    {
        Instantiate(spearDrop, transform.position, transform.rotation);
        ScoreManager.instance.EnemyAddPoint(1000);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }



}
