using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject garlicEnemy;
    public GameObject pumpkinEnemy;
    public GameObject carrotEnemy;
    public GameObject pumpkinBossEntity;
    public GameObject carrotBossEntity;

    public Transform spawnPointA;
    public Transform spawnPointB;
    public Transform spawnPointC;
    public Transform spawnPointD;

    bool isSpawning = false;

    public ScoreManager scoreManager;

    public int spawnCount = 0;
    public int spawnCap = 25;
    public int bossThreshold = 0; //set gap between bosses

    int spawnerRandom = 0;

    bool spawnPumpkinBoss = false;
    bool spawnCarrotBoss = false;
    bool spawnLimit = false;

    public bool pumpkinBossDead = false;
    public bool carrotBossDead = false;

    List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
        spawnPointA = GameObject.Find("spawnA").transform;
        spawnPointB = GameObject.Find("spawnB").transform;
        spawnPointC = GameObject.Find("spawnC").transform;
        spawnPointD = GameObject.Find("spawnD").transform;

        spawnPoints.Add(spawnPointA);
        spawnPoints.Add(spawnPointB);
        spawnPoints.Add(spawnPointC);
        spawnPoints.Add(spawnPointD);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            if (spawnCount <= spawnCap)
            {
                isSpawning = true;
                StartCoroutine(Wait());
            }
            else
            {
                spawnLimit = true;
                UnityEngine.Debug.Log("Spawn Limit Reached!");
            }
        }
        if (scoreManager.score >= 5000)
        {
            if (!spawnPumpkinBoss)
            {
                spawnerRandom = Random.Range(0, 4);
                Transform pumpkinSpawner = spawnPoints[spawnerRandom];
                Instantiate(pumpkinBossEntity, pumpkinSpawner.position, transform.rotation);
                spawnPumpkinBoss = true;
            }
        }
        if (scoreManager.score >= (bossThreshold + 10000))
        {
            if (!spawnCarrotBoss)
            {
                spawnerRandom = Random.Range(0, 4);
                Transform carrotSpawner = spawnPoints[spawnerRandom];
                Instantiate(carrotBossEntity, carrotSpawner.position, transform.rotation);
                spawnCarrotBoss = true;
            }
        }

    }
    IEnumerator Wait()
    {
        foreach(Transform spawner in spawnPoints)
        {
            if (!spawnLimit)
            {
                int spawnPool = Random.Range(0, 101);
                UnityEngine.Debug.Log("Spawn pool: " + spawnPool);
                if (spawnPool >= 1 && spawnPool <= 50)
                {
                    Instantiate(garlicEnemy, spawner.position, spawner.rotation);
                }
                else if (spawnPool >= 51 && spawnPool <= 75)
                {
                    if (pumpkinBossDead)
                    {
                        UnityEngine.Debug.Log("Spawning pumpkin crawler");
                        Instantiate(pumpkinEnemy, spawner.position, spawner.rotation);
                    }
                    else
                    {
                        Instantiate(garlicEnemy, spawner.position, spawner.rotation);
                    }
                }
                else if (spawnPool >= 76 && spawnPool <= 100)
                {
                    if (carrotBossDead)
                    {
                        UnityEngine.Debug.Log("Spawning carrot squid");
                        Instantiate(carrotEnemy, spawner.position, spawner.rotation);
                    }
                    else
                    {
                        Instantiate(garlicEnemy, spawner.position, spawner.rotation);
                    }
                }
            }
            yield return new WaitForSeconds(2f);
        }

        isSpawning = false;
    }
}
