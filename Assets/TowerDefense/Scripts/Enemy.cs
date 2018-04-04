using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 10f;
	public float minDist = .2f;
	private Transform target;
	private int wavepointTdx = 0;

	void Start ()
	{
		target = Waypoints.wayPoints [0];

	}
	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate (dir.normalized*speed * Time.deltaTime, Space.World);

		if (Vector3.Distance (transform.position, target.position) <= minDist)
		{
			GetNextPoint ();
		}
	}
	void GetNextPoint ()
	{
		if (wavepointTdx >= Waypoints.wayPoints.Length - 1) {
			Destroy (gameObject);
			return;
		}
		wavepointTdx++;
		target = Waypoints.wayPoints [wavepointTdx];
	}

}
