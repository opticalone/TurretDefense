using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{

	private Transform target;

	public float speed = 80f;
	public GameObject impactEffect;

	public void seek(Transform _target)
	{
		target = _target;
	}
	

	void Update ()
	{
		if (target == null) {
			Destroy (gameObject);
			return;
		}	

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame) 
		{
		
			hitTarget ();
			return;
		}
		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}
	void hitTarget ()
	{
		GameObject effectInstance = (GameObject)Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);

	}
}
