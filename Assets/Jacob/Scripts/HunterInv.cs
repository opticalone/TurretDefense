using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterInv : MonoBehaviour {

    public int rabbitMeat;
    //public int rabbitMeatDrop;


	// Use this for initialization
	void Start () {
        rabbitMeat = 0;
	}
	
    //void Update()
    //{
    //    rabbitMeatDrop = rabbitMeat;
    //}

	public void collectRabbitMeat()
    {
        rabbitMeat++;
    }

    public void rabbitDropOff()
    {
        rabbitMeat = 0;
    }
}
