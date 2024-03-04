using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject garlicEnemy;
    public GameObject pumpkinEnemy;
    //public GameObject carrotEnemy;
    bool isSpawning = false;

    public PumpkinBoss pumpkinBoss;
    public ScoreManager scoreManager;
    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(Wait());
        }
        
    }
    IEnumerator Wait()
    {
        if (scoreManager.bossKills == 0)
        {
            yield return new WaitForSeconds(5);
            Instantiate(garlicEnemy, transform.position, transform.rotation);
        }
        else
        {
            int spawnPool = Random.Range(0, 9);
            yield return new WaitForSeconds(5);

            Instantiate(garlicEnemy, transform.position, transform.rotation);
            Instantiate(pumpkinEnemy, transform.position, transform.rotation);

            /*if (spawnPool >= 0 && spawnPool <= 5)
            {
                Instantiate(garlicEnemy, transform.position, transform.rotation);
            }
            if (pumpkinBoss.isDead)
            {
                if (spawnPool >= 6 && spawnPool <= 8)
                {
                    Instantiate(pumpkinEnemy, transform.position, transform.rotation);
                }
            }*/
        }
        
        /*else if (spawnPool >= 10 && spawnPool <= 12)
        {
            Instantiate(carrotEnemy, transform.position, transform.rotation);
        }*/
        isSpawning = false;
    }
}
