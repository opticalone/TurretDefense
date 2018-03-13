using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    float horizontal;
    float vertical;
    float strafe;
    float jump;
    float attack;
    Rigidbody rb;

    public Camera cam;
    public float movementSpeed;
    public float jumpStrength;
    public float fallMultiplier = 2.5f;
    public float shortHopMultiplier = 2f;
    public bool shortHopAvailable;
    public float stoppingDrag;
    public static int gemsTotal;
    public int slamTimerAmount;
    public float slamSpeed;
    public bool isSlamming;
    public float attackTimerAmount;
    public bool isAttacking;
    public float strafeSpeed;
    public AnimationCurve brakeCurve;
    public bool devJump;

    private bool jumpQueued;
    private bool slamQueued;
    private int slamTimer;
    private float attackTimer;
    private bool attackQueued;
    private int smootherSpin;

	//UI for gem pick up

	public Text GemCount;


	//pancakes

	public int pancakes;

    //jump raycast
    public RaycastHit hit; //detecting raycast collision
    public float rayLength;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        jumpQueued = false;
        slamQueued = false;
        attackTimer = 0;
        devJump = false;
	}
    //bool sphereCastHit;
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        strafe = Input.GetAxis("Strafe");
        jump = Input.GetAxis("Jump");
        attack = Input.GetAxis("Fire1");

        if (attackTimer == 0)
        {
            attackQueued = false;
        }

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

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl) && !attackQueued && !isAttacking)
        {
            attackQueued = true;
            attackTimer = attackTimerAmount;
            smootherSpin = 0;
            if(!isAttacking)
            {
                isAttacking = true;
                Attack();
            }
          
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            devJump = true;
        }
    }
    float brakeTime;
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
        if (!isAttacking)
        {
            transform.rotation = rotation;
        }


        //movement
        if (vertical != 0 || strafe != 0)
        {
            Vector3 tempVector3 = new Vector3(0, rb.velocity.y, 0);
            rb.velocity = tempVector3;
            if (vertical != 0)
            {
                Vector3 desiredDir = targetDirection * movementSpeed;
                desiredDir.y = rb.velocity.y;
                rb.velocity = desiredDir;
            }
            if (strafe != 0 && !isAttacking)
            {
                if (strafe > 0)
                {
                    Vector3 temp = transform.right;
                    rb.velocity += temp * strafeSpeed;
                }
                if (strafe < 0)
                {
                    Vector3 temp = -transform.right;
                    rb.velocity += temp * strafeSpeed;
                }
            }
        }
        else //less slide on stop
        {
            brakeTime += Time.deltaTime;
            Vector3 temp = new Vector3(-rb.velocity.x, 0f, -rb.velocity.z);
            var extraBrake = brakeCurve.Evaluate(brakeTime);
            rb.AddForce(temp * stoppingDrag + (temp * extraBrake));
        }

        //Debug.Log(rb.velocity);

        //jump
        if (jumpQueued)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
			rb.AddForce (Vector3.forward * 1.5f, ForceMode.Impulse);
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


        isSlamming = false;
        if (slamQueued) Slam();


        if (devJump)
        {
            Vector3 temp = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
            rb.velocity = temp;
            devJump = false;
        }
    }

    //
    void Slam()
    {
        if (slamTimer > 0)
        {
            slamTimer--;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            isSlamming = true;
        }
        else
        {
            rb.useGravity = true;
            rb.AddForce(Vector3.down * slamSpeed, ForceMode.Impulse);
            isSlamming = true;
        }
    }


    IEnumerator spin()
    {
        float t = 0;
       
        Vector3 startRot = transform.forward;
        Vector3 desiredRot = Quaternion.AngleAxis( 90, Vector3.up) * transform.forward;
        int rotCounter = 0;
        while(rotCounter < 4)
        {
            while (t < 1)
            {
                t += Time.deltaTime * attackTimer;
                transform.forward = Vector3.Slerp(startRot, desiredRot, t);
                yield return null;
            }
            rotCounter++;
            startRot = desiredRot;
            desiredRot = Quaternion.AngleAxis(90, Vector3.up) * startRot;
            t = 0;
            yield return null;
        }

        attackQueued = false;
        isAttacking = false;
    }

    IEnumerator flip()
    {
        float t = 0;

        Vector3 startRot = transform.forward;
        Vector3 desiredRot = Quaternion.AngleAxis(90, Vector3.right) * transform.forward;
        int rotCounter = 0;
        while (rotCounter < 4)
        {
            while (t < 1)
            {
                t += Time.deltaTime * slamTimer;
                transform.forward = Vector3.Slerp(startRot, desiredRot, t);
                yield return null;
            }
            rotCounter++;
            startRot = desiredRot;
            desiredRot = Quaternion.AngleAxis(90, Vector3.right) * startRot;
            t = 0;
            yield return null;
        }
    }


    void Attack()
    {
        StopCoroutine(spin());
        StartCoroutine(spin());
    }

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Gem"))
		{
			gemsTotal++;
			GemCount.text = gemsTotal.ToString ();
			other.gameObject.SetActive (false);
		}
	}
}  
