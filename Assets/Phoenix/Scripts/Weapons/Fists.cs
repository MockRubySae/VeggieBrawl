using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fists : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<GarlicEnemy>(out GarlicEnemy enemy))
        {
            enemy.health = enemy.health - 1;
        }
    }
}
