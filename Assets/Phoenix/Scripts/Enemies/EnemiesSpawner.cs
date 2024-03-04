using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject garlicEnemy;
    bool isSpawning = false;
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
        yield return  new WaitForSeconds(5);
        Instantiate(garlicEnemy, transform.position, transform.rotation);
        isSpawning = false;
    }
}
