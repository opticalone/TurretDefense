using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour {

    public GameObject player;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        player.GetComponent<PlayerController>().isOnLedge = true;
        player.GetComponent<PlayerController>().ableToJump = true;
    }

    void OnTriggerLeave(Collider other)
    {
        rb.useGravity = true;
        player.GetComponent<PlayerController>().isOnLedge = false;
        player.GetComponent<PlayerController>().ableToJump = false;
    }
}
