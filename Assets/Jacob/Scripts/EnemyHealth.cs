using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour {

    public int MaxHealth = 1;
    public static int Health;
	
    void Start()
    {
        Health = MaxHealth;
    }

	void Update () {
		if (Health <= 0)
        {
            Destroy(this);
        }
	}

    public void takeDamage()
    {
        Health--;
    }
}
