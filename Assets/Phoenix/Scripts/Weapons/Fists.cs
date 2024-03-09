using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fists : MonoBehaviour
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
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<GarlicEnemy>(out GarlicEnemy garlicMob))
        {
            garlicMob.health = garlicMob.health - player.strength;
        }
        else if (collision.gameObject.TryGetComponent<PumpkinEnemy>(out PumpkinEnemy pumpkinMob))
        {
            pumpkinMob.health = pumpkinMob.health - player.strength;
        }
        else if (collision.gameObject.TryGetComponent<CarrotEnemy>(out CarrotEnemy carrotMob))
        {
            carrotMob.health = carrotMob.health - player.strength;
        }
        else if (collision.gameObject.TryGetComponent<PumpkinBoss>(out PumpkinBoss pumpkinBossMob))
        {
            Debug.Log("Boss hit!");
            pumpkinBossMob.health = pumpkinBossMob.health - player.strength;
            Debug.Log(pumpkinBossMob.health);
        }
        else if (collision.gameObject.TryGetComponent<CarrotBoss>(out CarrotBoss carrotBossMob))
        {
            Debug.Log("Boss hit!");
            carrotBossMob.health = carrotBossMob.health - player.strength;
            Debug.Log(carrotBossMob.health);
        }
    }
}
