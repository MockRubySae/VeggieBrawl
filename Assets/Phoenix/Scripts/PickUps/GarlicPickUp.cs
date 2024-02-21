using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicPickUp : MonoBehaviour
{
    public GarlicFillAmount garlic;
    public Movement player;
    // rigidbody refreance
    Rigidbody rb;
    // make speed half of player

    

    void Start()
    {
        // give rigid body
        rb = GetComponent<Rigidbody>();

        garlic = GameObject.Find("GarlicMutationManager").GetComponent<GarlicFillAmount>();
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        // make the enemy look  at camera while standing
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            StartCoroutine(Eating());
            Debug.Log("isEaing");
        }
    }
    IEnumerator Eating()
    {
        Debug.Log("starting to eat");
        player.speed = 0f;
        yield return new WaitForSeconds(5);
        garlic.GarlicGain(10);
        player.speed = 10f;
        Destroy(gameObject);
        Debug.Log("finished eatingt");
    }

}
