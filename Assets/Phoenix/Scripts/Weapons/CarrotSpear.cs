using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CarrotSpear : MonoBehaviour
{
    public PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
        StartCoroutine(DestroyAfter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<GarlicEnemy>(out GarlicEnemy garlicMob))
        {
            garlicMob.health = garlicMob.health - (3 + player.strength);
        }
        else if (collision.gameObject.TryGetComponent<PumpkinEnemy>(out PumpkinEnemy pumpkinMob))
        {
            pumpkinMob.health = pumpkinMob.health - (3 + player.strength);
        }
        else if (collision.gameObject.TryGetComponent<PumpkinBoss>(out PumpkinBoss pumpkinBossMob))
        {
            Debug.Log("Boss hit!");
            pumpkinBossMob.health = pumpkinBossMob.health - (3 + player.strength);
            Debug.Log(pumpkinBossMob.health);
        }
        else if (collision.gameObject.TryGetComponent<PumkinEnemy>(out PumkinEnemy pumkinEnemy))
        {
            pumkinEnemy.health = pumkinEnemy.health - (3 + player.strength);
        }
        else if (collision.gameObject.TryGetComponent<PumkinEnemyBoss>(out PumkinEnemyBoss pumkinEnemyBoss))
        {
            pumkinEnemyBoss.health = pumkinEnemyBoss.health - (3 + player.strength);
        }
    }
}
