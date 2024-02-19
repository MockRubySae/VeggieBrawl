using UnityEngine;

public class Movement : MonoBehaviour
{
    public ParticleSystem dust;
    // set the speed to 10
    private float speed = 10.0f;
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
        Sprint();
        // check if the player is moving
        if (horizontalInput != 0 || verticalInput != 0)
        {
            // play dust particle
            dust.Play();
        }
        else
        {
            // if the player is not moving dont play the dust particle
            dust.Stop();
        }
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
    }
    void Sprint()
    {
        // get the imput of shift
        if(Input.GetKey(KeyCode.LeftShift))
        {
            // make speed 1.5 times bigger
            speed = 15f;
        }
        // when the shift key is not pressed return speed to normal
        else
        {
            // set speed to normal
            speed = 10f;
        }
        
    }
}
