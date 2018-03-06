using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour {

    public Transform[] Waypoints;
    public float speed = 2;

    public int CurrentPoint = 0;

	Component objectCollider;

    //Rigidbody platformRB;
   // Rigidbody playerRB;

    void Start()
    {
		objectCollider = GetComponentInChildren<MeshCollider> ();
    //    platformRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position != Waypoints[CurrentPoint].transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentPoint].transform.position, speed * Time.deltaTime);
        }

        if (transform.position == Waypoints[CurrentPoint].transform.position)
        {
            CurrentPoint += 1;
        }
        
        if (CurrentPoint >= Waypoints.Length)
        {
            CurrentPoint = 0;
        }
    }
}
