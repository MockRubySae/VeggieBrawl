using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject fists;
    public float speedOfFists = 1500f;
    public PlayerStats playerStats;
    bool isAttacking = false;
    bool isFists = false;
    bool currentlyAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isFists = true;
            isAttacking = true; 
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        { 
          isFists = false; 
          isAttacking = false;
        }
        if ( isAttacking && isFists)
        {
            if( !currentlyAttacking )
            {
                StartCoroutine(AttackSpeedOfFists());
            }
            
        }
    }
    public void Fists()
    {
        GameObject currentFists = (GameObject)Instantiate(fists, transform.position, transform.rotation);
        currentFists.GetComponent<Rigidbody>().AddForce(currentFists.transform.forward * speedOfFists);
    }
    IEnumerator AttackSpeedOfFists()
    {
        currentlyAttacking = true;
        Fists();
        yield return new WaitForSeconds(0.5f/playerStats.AttackSpeed);
        currentlyAttacking = false;
    }
}
