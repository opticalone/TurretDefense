using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float horizontal;
    float vertical;
    Rigidbody rb;

    public Camera cam;
    public float movementSpeed;
    public float maxSpeed;

    private float yAxisMovement;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //find movement direction
        float yAxisMovement = rb.velocity.y;
        Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);
        targetDirection = cam.transform.TransformDirection(targetDirection);
        targetDirection.y = 0f;

        //find direction the chracter should be facing
        Vector3 relativePos = -(cam.transform.position - transform.position);
        relativePos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

        //movement
        if (horizontal != 0 || vertical != 0)
        {
            rb.AddForce(targetDirection * movementSpeed);
        }
    }
}
