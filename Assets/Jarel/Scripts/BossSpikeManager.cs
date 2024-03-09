using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpikeManager : MonoBehaviour
{
    public GameObject bossSpike;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateSpikes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CreateSpikes()
    {
        //creates wave of spikes
        yield return new WaitForSeconds(0.2f);
        for (int x = 0; x < 12; x++)
        {
            GameObject bossSpikeAttacker = (GameObject)Instantiate(bossSpike, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
