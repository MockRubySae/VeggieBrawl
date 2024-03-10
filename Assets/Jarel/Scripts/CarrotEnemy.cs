using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class CarrotEnemy : MonoBehaviour
{
    public PlayerStats stats;
    // make a refreance to the players posision 
    public Transform playerPos;
    public Transform carrotEntity;
    public float health = 4;
    bool isAttacking = false;
    bool isMoving = true;
    public bool isDead = false;
    
    // rigidbody refreance
    Rigidbody rb;

    public Collider pCollider; //reference to physics collider
    // make speed half of player
    float speed = 3.0f;
    float digSpeed = 5.0f;

    public GameObject spike;
    public float speedOfSpike = 1500f;

    private Animator spriteAnimComp;

    private bool inRange = false;

    public EnemiesSpawner spawnCounter;

    void Start()
    {
        // give rigid body
        
        rb = GetComponent<Rigidbody>();
        playerPos = GameObject.Find("playerNormal").transform;
        stats = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
        spawnCounter = GameObject.Find("SpawnPoints").GetComponent<EnemiesSpawner>();
        spriteAnimComp = GetComponent<Animator>();
        carrotEntity = GetComponent<Transform>();
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
            if (isMoving) //prevent transform.position conflict with attack behavior
            {
                var step = speed * Time.deltaTime;
                // move from current position to the position of the player
                transform.position = Vector3.MoveTowards(transform.position, playerPos.transform.position, step);
            }
        }

        if (health <= 0)
        {
            isMoving = false;
            isDead = true;
            spriteAnimComp.Play("carrotSquid_die");
            StartCoroutine(DestroyEntity());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            // Trigger the attack behavior here when the player enters the trigger zone
            if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                inRange = true;
                Debug.Log("Player detected, triggering attack behavior!");
                if (!isAttacking && health > 0)
                {
                    isMoving = false;
                    isAttacking = true;
                    speed = 0f;
                    spriteAnimComp.Play("carrotSquid_attackDig");
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

    void CallDig() //keyframe event
    {
        rb.useGravity = false;
        pCollider.enabled = false;
        StartCoroutine(CarrotDig());
    }

    IEnumerator CarrotDig()
    {
        float targetDig = -5f;
        float dig = digSpeed * Time.deltaTime;
        while (transform.position.y > targetDig)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetDig, transform.position.z), dig);
            yield return null;
            Debug.Log("yield return null");
        }
        yield return new WaitForSeconds(3.0f);
        transform.position = new Vector3(playerPos.transform.position.x, transform.position.y, playerPos.transform.position.z);
        spriteAnimComp.Play("carrotSquid_attackSpike");
    }

    void CallAttack() //keyframe event
    {
        StartCoroutine(CarrotAttack());
    }

    IEnumerator CarrotAttack()
    {
        bool spikeLaunch = false;
        float dig = (digSpeed * 3f) * Time.deltaTime;

        while (transform.position.y < 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2f, transform.position.z), dig);
            yield return null;

            if (transform.position.y > -4f && !spikeLaunch) //just for timing reasons
            {
                GameObject carrotSpike = (GameObject)Instantiate(spike, transform.position, transform.rotation);
                carrotSpike.GetComponent<Rigidbody>().AddForce(Vector3.up * speedOfSpike);
                spikeLaunch = true;
            }
        }
        rb.useGravity = true;
        pCollider.enabled = true;
        isMoving = true;
        speed = 3f;
        spriteAnimComp.Play("carrotSquid_move");
        yield return new WaitForSeconds(3.0f); //cooldown before next attack
        isAttacking = false; //dont swap for is moving; seems to mess up intended behavior
        ContinueAttack();
    }

    void ContinueAttack()
    {
        if (inRange)
        {
            isMoving = false;
            isAttacking = true;
            spriteAnimComp.Play("carrotSquid_attackDig");
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
        spawnCounter.spawnCount--;
        StartCoroutine(DestroyEntity());
    }

    IEnumerator DestroyEntity()
    {
        //   int changeToDrop = Random.Range(0, 10);
        //   if (changeToDrop >= 8)
        //   {
        //        Instantiate(garlicDrop, transform.position, transform.rotation);
        //   }
        ScoreManager.instance.EnemyAddPoint(300);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

}
