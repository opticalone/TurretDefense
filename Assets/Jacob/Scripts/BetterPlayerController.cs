using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerController : MonoBehaviour {

    //public variables
    public float walkspeed;

    //private variables
    Rigidbody rb;
    Vector3 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 yVelFix = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDirection * walkspeed * Time.deltaTime;
        rb.velocity += yVelFix;
    }
}
