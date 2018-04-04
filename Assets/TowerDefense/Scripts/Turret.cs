using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour 
{

	private Transform target;

	[Header("a t t r i b u t e s")]

	public float range =15f;
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("p r e f a b s")]

	public string enemyTag = "Enemy";

	public Transform partToRotate;

	public GameObject bulletPrefab;
	public Transform firePoint;





	void Start()
	{
		InvokeRepeating ("UpdateTarget", 0f, .5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag (enemyTag);

		float shortestDistance = Mathf.Infinity;

		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies) 
		{
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);

			if (distanceToEnemy <shortestDistance) 
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}

		}

		if (nearestEnemy != null && shortestDistance <= range) {

			target = nearestEnemy.transform;
		
		} 
		else
		{
			target = null;
		}
	}

	void Update()
	{
		if (target == null)
			return;
			
		//Vector3 dir = target.position - transform.position;
		partToRotate.LookAt(target);
	
		if (fireCountdown <= 0f) {
			Shoot ();
			fireCountdown = 1f / fireRate;
		}
		fireCountdown -= Time.deltaTime;
	}

	void Shoot ()
	{
		GameObject bulletObj =(GameObject)Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletObj.GetComponent<Bullet> ();

		if (bullet != null) 
		{
		
			bullet.seek (target);
		
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, range);
	}


}
