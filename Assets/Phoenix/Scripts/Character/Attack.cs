using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject fists;
    public float speedOfFists = 1500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Fists();
        }
    }
    public void Fists()
    {
        GameObject currentFists = (GameObject)Instantiate(fists, transform.position, transform.rotation);
        currentFists.GetComponent<Rigidbody>().AddForce(currentFists.transform.forward * speedOfFists);
    }
}
