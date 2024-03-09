using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject pumpkinCrawlerBoss;
    public GameObject carrotSquidBoss;

    private GameObject spawnPointA;

    private ScoreManager bossManager;

    private bool spawnPumpkinBoss = false;
    private bool spawnCarrotBoss = false;
    private Transform bossSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointA = GameObject.Find("Cube");
        bossManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        bossSpawnPoint = spawnPointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossManager.score >= 5000)
        {
            if(!spawnPumpkinBoss)
            {
                Instantiate(pumpkinCrawlerBoss, bossSpawnPoint.transform.position, transform.rotation);
                spawnPumpkinBoss = true;
            }
        }
        if (bossManager.score >= 10000)
        {
            if (!spawnCarrotBoss)
            {
                Instantiate(carrotSquidBoss, bossSpawnPoint.transform.position, transform.rotation);
                spawnCarrotBoss = true;
            }
        }
    }
}
