using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float horizontal;
    float vertical;
    float jump;
    Rigidbody rb;

    public Camera cam;
    public float movementSpeed;
    public float maxSpeed;
    public float jumpStrength;
    public bool ableToJump; 

    private float yAxisMovement;
    private float jumpTimer;

    //jump raycast
    private RaycastHit hit; //detecting raycast collision
    public float rayLength; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.up * -rayLength, Color.red, 0);

        if (Physics.Raycast(transform.position, transform.up * -1, out hit, rayLength))
        {
            if (hit.collider.gameObject.layer == 8)
            {
                Debug.Log(hit.collider.name);

                if (Input.GetAxis("Jump") == 1)
                {
                    rb.AddForce(Vector3.up * jumpStrength);
                }
            }
        }
	}

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");

        //find movement direction
        Vector3 targetDirection = new Vector3(0.0f, 0.0f, vertical);
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
