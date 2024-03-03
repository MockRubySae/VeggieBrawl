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
        if(collision.gameObject.TryGetComponent<GarlicEnemy>(out GarlicEnemy enemy))
        {
            enemy.health = enemy.health - 4 * player.strength;
        }
    }
}
