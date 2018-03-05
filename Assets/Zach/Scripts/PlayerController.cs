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
    public bool onLedge;
    public int gems;

    private bool jumpQueued;

    //jump raycast
    public RaycastHit hit; //detecting raycast collision
    public float rayLength; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        jumpQueued = false;
        ableToJump = false;
	}
    bool sphereCastHit;
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");

        RaycastHit sphereHit;
        if(Physics.SphereCast(transform.position,.2f, transform.forward,out sphereHit,2,1 << 8))
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            sphereCastHit = true;
            onLedge = true;
            Debug.Log("Hit infront of me");
            //find direction of the edge
            Vector3 vecToLookAt = hit.point - this.transform.position;
            //Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);
            Debug.DrawLine(this.transform.position, (transform.position + sphereHit.point.normalized), Color.red);
            transform.LookAt(transform.position + sphereHit.point.normalized);
            cam.transform.LookAt(transform.position + sphereHit.point.normalized);
            //Debug.DrawRay(hit.point, reflectVec, Color.green);
        } else
        {
            sphereCastHit = false;
            rb.useGravity = true;
            onLedge = false;
        }





        //Drawing Raycast for player's jump
        Debug.DrawRay(transform.position, transform.up * -rayLength, Color.red, 0);

        //Using Raycast to check if the object below it within a certain range is able to be jumped off of

       //jumpCast = Physics.Raycast(transform.position, transform.up * -1, out hit, rayLength);
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, rayLength))
        {
            if (hit.collider.gameObject.layer == 8)
            {
                //using a bool so that all physics updates are in FixedUpdate
                if (Input.GetAxis("Jump") == 1)
                {
                    jumpQueued = true;
                    ableToJump = false;
                }
            }
        }

        //if (ableToJump)
        //{
        //    //using a bool so that all physics updates are in FixedUpdate
        //    if (Input.GetAxis("Jump") == 1)
        //    {
        //        ableToJump = false;
        //        ledgeClimbing = true;
        //    }
        //}
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
        if (horizontal != 0 || vertical != 0 )
        {
            Vector3 desiredVelocity = targetDirection * movementSpeed;
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
            if (!onLedge)
            {
                rb.AddForce(desiredVelocity);
            }
            
        }

        else //less slide on stop
        {
            Vector3 temp = new Vector3(-rb.velocity.x, 0f, -rb.velocity.z);
            rb.AddForce(temp * stoppingDrag);
        }

        //jump
        if (jumpQueued)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jumpQueued = false;
        }

        //Crisper Jump
        if (rb.velocity.y < 0 && rb.useGravity)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && Input.GetAxis("Jump") != 1 && shortHopAvailable == true && rb.useGravity)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (shortHopMultiplier) * Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        if (sphereCastHit)
            Gizmos.color = Color.blue;
        else Gizmos.color = Color.red;
        for(int i =0; i < 2; i++)
        {
            Gizmos.DrawWireSphere(transform.position + (transform.forward * i), .5f);
        }
       
    }

}  
