using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SeedBullet : MonoBehaviour
{
    public PlayerStats player;
    private bool bulletHit = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
        StartCoroutine(DestroyAfter());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(2f);
        if (!bulletHit)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Seed hit");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            bulletHit = true;
            //Debug.Log("Player hit for 4 damage!");
            player.health = player.health - 2;
            Destroy(gameObject);
        }
    }
}
