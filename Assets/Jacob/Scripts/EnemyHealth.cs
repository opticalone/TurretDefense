using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour {

    public int MaxHealth = 1;
<<<<<<< HEAD
    public int Health;
    GameObject Player;
    bool invincibleFrams = false;
    public float invincibleTime;
    float timeStart;
    Component bodyCollider;
=======
    public static int Health;
>>>>>>> dev
	
    void Start()
    {
        timeStart = invincibleTime;
        Health = MaxHealth;
        Player = GameObject.FindGameObjectWithTag("Player");
        bodyCollider = GetComponentInParent<CapsuleCollider>();
    }

	void Update () {
        Debug.Log(Health);
        if(invincibleFrams == true)
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime <= 0)
            {
                invincibleTime = timeStart;
                invincibleFrams = false;
            }
        }
		if (Health <= 0)
        {
            Destroy(this);
            Destroy(transform.parent.gameObject);
        }
	}

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("Working");
        if (c.gameObject == Player)
        {
            if (invincibleFrams == false)
            {
                Health -= 1;
                invincibleFrams = true;
            }
        }
    }
}
