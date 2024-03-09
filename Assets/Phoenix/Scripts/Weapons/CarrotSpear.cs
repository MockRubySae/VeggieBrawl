using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CarrotSpear : MonoBehaviour
{
    public PlayerStats player;
    float playerDmg;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
        playerDmg = player.strength + 2f;
        StartCoroutine(DestroyAfter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<GarlicEnemy>(out GarlicEnemy garlicMob))
        {
            garlicMob.health = garlicMob.health - playerDmg;
        }
        else if (collision.gameObject.TryGetComponent<PumpkinEnemy>(out PumpkinEnemy pumpkinMob))
        {
            pumpkinMob.health = pumpkinMob.health - playerDmg;
        }
        else if (collision.gameObject.TryGetComponent<CarrotEnemy>(out CarrotEnemy carrotMob))
        {
            carrotMob.health = carrotMob.health - playerDmg;
        }
        else if (collision.gameObject.TryGetComponent<PumpkinBoss>(out PumpkinBoss pumpkinBossMob))
        {
            Debug.Log("Boss hit!");
            pumpkinBossMob.health = pumpkinBossMob.health - playerDmg;
            Debug.Log(pumpkinBossMob.health);
        }
        else if (collision.gameObject.TryGetComponent<CarrotBoss>(out CarrotBoss carrotBossMob))
        {
            Debug.Log("Boss hit!");
            carrotBossMob.health = carrotBossMob.health - playerDmg;
            Debug.Log(carrotBossMob.health);
        }
    }
}
