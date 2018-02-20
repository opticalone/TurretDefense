//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class HunterReturn : MonoBehaviour {

//    public GameObject homeBase;
//    NavMeshAgent agent;
//    QuinnGun ammo;
//    HunterInv meat;
//    Health health;
//    GameObject Hunter;
//    public float hunterSpeed;
//    public int runHealth = 5;
//    float speed;
//    int maxHealth;
//    int currentHealth;
//    int totalAmmo;
//    int totalMeat;
//    int ammoMax;
//    HunterChase chase;
//    HunterWander wander;

//    // Use this for initialization
//    void Start () {
//        agent = GetComponent<NavMeshAgent>();
//       // GameObject homeBase = GameObject.Find("HunterCamp");
//        GameObject Hunter = GameObject.Find("Hunter");
//        agent.speed = hunterSpeed;
//        speed = hunterSpeed;
//        health = Hunter.GetComponent<Health>();
//        ammo = Hunter.GetComponent<QuinnGun>();
//        meat = Hunter.GetComponent<HunterInv>();
//        maxHealth = health.MaxHP;
//        currentHealth = health.CurrentHP;
//        ammoMax = ammo.MaxAmmoReserve;
//        totalMeat = meat.rabbitMeat;
//        chase = GetComponent<HunterChase>();
//        wander = GetComponent<HunterWander>();
//    }
	
//	// Update is called once per frame
//	void Update () {
//        if (ammo.AmmoReserve <= 0 || meat.rabbitMeat >= 5 || health.CurrentHP <= runHealth)
//        {
           
//            if(chase != null)
//            {
//                chase.enabled = false;
//                chase.chaseTarget = null;
//            }
//            if(wander != null)
//            {
//                wander.enabled = false;
//                wander.target = null;
//            }

//            //GetComponent<HunterChase>().enabled = false;
//            //GetComponent<HunterWander>().enabled = false;
//            //Hunter.GetComponent<HunterChase>().chaseTarget = null;
//            //Hunter.GetComponent<HunterWander>().target = null;
//            agent.destination = homeBase.transform.position;

//            if (totalAmmo == ammoMax && currentHealth == maxHealth)
//            {
//                speed = hunterSpeed;
//                wander.enabled = true;

//            }
//        }

//        if (currentHealth <= runHealth)
//        {
//            GetComponent<HunterWander>().enabled = false;
//            agent.destination = homeBase.transform.position;
//            agent.speed = agent.speed * 2;

//            //if (totalAmmo == ammoMax && currentHealth == maxHealth)
//            //{
//            //    speed = hunterSpeed;
//            //    GetComponent<HunterWander>().enabled = true;

//            //}
//        }
//	}
//}
