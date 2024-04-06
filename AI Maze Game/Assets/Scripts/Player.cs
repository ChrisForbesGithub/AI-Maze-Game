using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour


{
    public static Player player;
    public float moveSpeed;
    public float camSpeed;
    public float maxSpeed;


    public Transform camRig;

    // Start is called before the first frame update
    void Start()
    {
        player = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 200);
        }

        camRig.Rotate(Vector3.up * Input.GetAxis("Mouse X") * camSpeed);
        camRig.position = transform.position;
        Move();

        if (GameManager.gm.slowTimer > 0)
        {
            moveSpeed = 125f;
        }
        else if (GameManager.gm.boostTimer > 0)
        {
            moveSpeed = 2000f;
        }
        else
        {
            moveSpeed = 500;
        }

    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;
        movement += camRig.forward * Input.GetAxis("Vertical");
        movement += camRig.right * Input.GetAxis("Horizontal");
        movement.y = 0;
        movement.Normalize();
        GetComponent<Rigidbody>().AddForce(movement * Time.deltaTime * moveSpeed);

        movement = GetComponent<Rigidbody>().velocity;
        movement.y = 0;
        movement = Vector3.ClampMagnitude(movement, maxSpeed);
        movement.y = GetComponent<Rigidbody>().velocity.y;
        GetComponent<Rigidbody>().velocity = movement;

    }
}
