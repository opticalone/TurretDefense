using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGems : MonoBehaviour {

    PlayerController pc;
    public GameObject player;

	// Use this for initialization
	void Start () {
        pc = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HIT");
            pc.gems++;
            Destroy(gameObject);
        }
    }
}
