using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public ParticleSystem dust;
    // set the speed to 10
    public float speed = 10.0f;
    // add horziontal input for basic controls
    private float horizontalInput;
    // do the same but for vertical movement
    private float verticalInput;
    // adds direction to the player
    private Vector3 movedirection;
    // make are player a game object and visble to everything
    public GameObject player;
    // add rigid body 
    Rigidbody rb;
    public bool isWalking = false;
    public bool isSprinting = false;
    public bool isToggleSprint = false;

    public Animator playerAnimator;
    public Attack attackManager;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody>();
        if(dust != null)
        {
            dust.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is sprinting
        if (!isToggleSprint)
        {
            Sprint();
        }
        else if (isToggleSprint)
        {
            SprintToggle();
        }
        Walking();
        // get the axis for horzontal movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // get the axis for vertical
        verticalInput = Input.GetAxisRaw("Vertical");
        // add move direction \
        // normalize the direction so the player stays at a constant speed even when moving diagonal
        movedirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        // fix shit so that the player stops floating
        // also make the speed not dependent on frame rate
        // move the possition of the player
        transform.position += movedirection * speed * Time.deltaTime;

        if (attackManager.currentlyAttacking)
        {
            if (isWalking)
            {
                if ((Input.GetKeyDown(KeyCode.LeftShift)) || (Input.GetKey(KeyCode.LeftShift)))
                {
                    playerAnimator.Play("player_sprint");
                }
                else
                {
                    playerAnimator.Play("player_walk");
                }
            }
            else
            {
                playerAnimator.Play("player_idle");
            }
        }
    }
    void SprintToggle()
    {
        // get the imput of shift
        if(Input.GetKeyDown(KeyCode.LeftShift) && speed != 0 && isSprinting == false)
        {
            // make speed 1.5 times bigger
            speed = 15f;
            isSprinting = true;
        }
        // when the shift key is not pressed return speed to normal
        else if(Input.GetKeyDown(KeyCode.LeftShift) && speed != 0 && isSprinting == true)
        {
            // set speed to normal
            speed = 10f;
            isSprinting = false;
        }
        
    }
    void Sprint()
    {
        // get the imput of shift
        if (Input.GetKey(KeyCode.LeftShift) && speed != 0 && isSprinting == false)
        {
            // make speed 1.5 times bigger
            speed = 15f;
            isSprinting = true;
        }
        // when the shift key is not pressed return speed to normal
        else if (speed != 0)
        {
            // set speed to normal
            speed = 10f;
            isSprinting = false;
        }

    }
    void Dust() //called in keyframe as event
    {
        StartCoroutine(DustCycle());
    }
    private IEnumerator DustCycle()
    {
        dust.Play();
        yield return new WaitForSeconds(0.5f);
        dust.Stop();
    }

    void Walking()
    {
        if (horizontalInput != 0 || verticalInput != 0 )
        {
            if(isSprinting == false && speed != 0)
            {
                isWalking = true;
            }
        }
        else if (horizontalInput == 0 || verticalInput == 0 || speed == 0)
        {
            isWalking = false;
        }
    }
}
