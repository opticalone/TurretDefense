using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterChase : MonoBehaviour
{

    public GameObject chaseTarget;
    NavMeshAgent agent;
    public float distance;
    float gunDistance;
    float viewRange;
    public int stoppingDistance;
    //int targetHealth;
    //QuinnGun gun;
    Vector3 fwd;
    public float length;

    HunterWander hunterWander;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject Hunter = GameObject.Find("Enemie");
        hunterWander = Hunter.GetComponent<HunterWander>();
        //gun = Hunter.GetComponent<QuinnGun>();
        chaseTarget = hunterWander.target;
        //gunDistance = gun.RayCastLength;
        viewRange = hunterWander.length;
        fwd = transform.TransformDirection(Vector3.forward);
        length = stoppingDistance + 3;
    }

    void OnEnable()
    {
        chaseTarget = hunterWander.target;
    }

    // Update is called once per frame
    void Update()
    {
        int numOfRays = 15;
        fwd = transform.forward;
        fwd = Quaternion.AngleAxis(-45, Vector3.up) * fwd;
        for (int i = 0; i < numOfRays; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fwd * length, out hit, length))
            {
                if (hit.collider.gameObject.tag == "DeadAnimal")
                {
                    agent.stoppingDistance = 0;
                }
            }
            Debug.DrawRay(transform.position, fwd * length, Color.yellow);
            fwd = Quaternion.AngleAxis(90 / numOfRays, Vector3.up) * fwd;
        }
        //targetHealth = chaseTarget.GetComponent<Health>().CurrentHP;
        agent.destination = chaseTarget.transform.position;
        distance = Vector3.Distance(transform.position, chaseTarget.transform.position);
        //targetHealth = hunterWander.targetHealth;
        //searching for dead animal
                
         // interacting with live animal

         if (chaseTarget != null)
         {
             agent.stoppingDistance = stoppingDistance;
         }

         //if (targetHealth == 0)
         //{
         //    agent.stoppingDistance = 0;
         //}

         //if (distance <= gunDistance)
         //{
         //    QuinnGun Fire = gameObject.GetComponent<QuinnGun>();
         //    gun.Fire();
         //}

         if (distance > viewRange || chaseTarget == null)
         {
             hunterWander.enabled = true;
             hunterWander.target = null;
             this.enabled = false;
             chaseTarget = null;
         }
     }
 }