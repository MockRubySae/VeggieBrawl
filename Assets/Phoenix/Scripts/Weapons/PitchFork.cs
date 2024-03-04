using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PitchFork : MonoBehaviour
{
    public PlayerStats player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerStats>();
        StartCoroutine(DestroyAfter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<GarlicEnemy>(out GarlicEnemy enemy))
        {
            enemy.health = enemy.health - 2 * player.strength;
        }
    }
}
