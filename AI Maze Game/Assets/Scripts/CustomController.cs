using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomController : MonoBehaviour
{
    //these are responsible for our movement speed
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    [SerializeField] private float crouchSpeed;

    //this will track the camera anchor object and let it follow the player
    [SerializeField] private GameObject cameraAnchor;

    [SerializeField] private LayerMask GroundMask;

    //this will hold the player object's rigidbody
    private Rigidbody rb;

    //this will hold the player's inputs this Update loop
    private Vector2 inputThisFrame;

    //this will hold our calculated movement
    private Vector3 movementThisFrame;

    // Start is called before the first frame update
    void Start()
    {
        //get the rigidbody component from the player
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //check what input we have this frame
        inputThisFrame = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //reset our movement to (0,0,0) by default
        movementThisFrame = new Vector3();

        //map our horitzonal movement based on our inputs
        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        float speedThisFrame = walkSpeed;

        if (Input.GetButton("Sprint"))
        {
            speedThisFrame = runSpeed;
        }
        else
            if (Input.GetButton("Crouch"))
        {
            speedThisFrame = crouchSpeed;
        }

        //multiply that direction by our speed
        movementThisFrame *= speedThisFrame;

        movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;

        //check if we're on the ground
        if (IsGrounded()) //this will be true or false
        {
            //if we are, check if the jump button is pressed
            if (Input.GetButtonDown("Jump"))
            {
                //if it is, set our y direction to our jump power
                movementThisFrame.y = jumpPower;
            }
        }


        //move our player object
        Move(movementThisFrame);
        //move the camera anchor to snap to the player position
        /*cameraAnchor.transform.position = transform.position;*/
    }

    virtual protected void Move(Vector3 direction)
    {
        rb.velocity = direction;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.001f, GroundMask);
    }

}
