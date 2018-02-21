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
    public float fallMultiplier = 2.5f;
    public float shortHopMultiplier = 2f;
    public bool shortHopAvailable;
    public float stoppingDrag;

    private float yAxisMovement;
    private float jumpTimer;
    private bool jumpQueued;

    //jump raycast
    private RaycastHit hit; //detecting raycast collision
    public float rayLength; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        jumpQueued = false;
	}
	
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");
        
        //Drawing Raycast for player's jump
        Debug.DrawRay(transform.position, transform.up * -rayLength, Color.red, 0);

        //Using Raycast to check if the object below it within a certain range is able to be jumped off of
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, rayLength))
        {
            if (hit.collider.gameObject.layer == 8)
            {
                Debug.Log(hit.collider.name);

                //using a bool so that all physics updates are in FixedUpdate
                if (Input.GetAxis("Jump") == 1)
                {
                    jumpQueued = true;
                }
            }
        }        
    }

    void FixedUpdate()
    {
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
            Vector3 desiredVelocity = targetDirection * movementSpeed;
            rb.AddForce(desiredVelocity);
            if (rb.velocity.x >= maxSpeed)
            {
                Vector3 temp = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
                rb.velocity = temp;
            }
            if (rb.velocity.z >= maxSpeed)
            {
                Vector3 temp = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
                rb.velocity = temp;
            }
        }
        else //less slide on stop
        {
            Vector3 temp = new Vector3(-rb.velocity.x, 0f, -rb.velocity.z);
            rb.AddForce(temp * stoppingDrag);
        }

        //
        if (jumpQueued)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jumpQueued = false;
        }
        
        //Crisper Jump
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && Input.GetAxis("Jump") != 1 && shortHopAvailable == true)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (shortHopMultiplier) * Time.deltaTime;
        }
    }
}  
