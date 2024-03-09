using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpikes : MonoBehaviour
{
    // Start is called before the first frame update

    Animator spriteAnimator;

    public PlayerStats player;
    void Start()
    {
        spriteAnimator = GetComponent<Animator>();
        spriteAnimator.Play("carrotBossSpikeAttack");
        player = GameObject.Find("playerNormal").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform, Vector3.up);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player hit");
        player.health = player.health - 2;
    }

    void CallDestroy()
    {
        Destroy(gameObject);
    }
}
