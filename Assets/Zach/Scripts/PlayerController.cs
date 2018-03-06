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
    public float fallMultiplier = 2.5f;
    public float shortHopMultiplier = 2f;
    public bool shortHopAvailable;
    public float stoppingDrag;
    public int gems;
    public int slamTimerAmount;
    public float slamSpeed;

    private bool jumpQueued;
    private bool slamQueued;
    private int slamTimer;

    //jump raycast
    public RaycastHit hit; //detecting raycast collision
    public float rayLength;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        jumpQueued = false;
        slamQueued = false;
	}
    //bool sphereCastHit;
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");

        //Drawing Raycast for player's jump to be seen in scene view
        Debug.DrawRay(transform.position, transform.up * -rayLength, Color.red, 0);

        //Using Raycast to check if the object below it within a certain range is able to be jumped off of
        if (Physics.Raycast(transform.position, transform.up * -1, out hit, rayLength))
        {
            if (hit.collider.gameObject.layer == 8)
            {
                //using a bool so that all physics updates are in FixedUpdate
                if (Input.GetAxis("Jump") == 1)
                {
                    jumpQueued = true;
                }
            }
            slamQueued = false;
        }
        // pauses in air and slams downward at an increased speed than gravity  
        else if (Input.GetKeyDown("space") && !slamQueued)
        {
            slamQueued = true;
            slamTimer = slamTimerAmount;
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
            Vector3 desiredDir = targetDirection * movementSpeed;
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
            rb.AddForce(desiredDir);

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
        if (slamQueued) Slam();
    }

    //
    void Slam()
    {
        if (slamTimer > 0)
        {
            slamTimer--;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.useGravity = true;
            rb.AddForce(Vector3.down * slamSpeed, ForceMode.Impulse);
        }
    }
}  
