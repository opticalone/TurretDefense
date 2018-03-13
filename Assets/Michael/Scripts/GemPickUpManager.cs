using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPickUpManager : MonoBehaviour {


	//need to do
	GameObject gem;
	//on trigger collect gem
	//disable gem game object
	//add gem to gem counter
	//add gem counter to player gems total

	// Use this for initialization
	void OnTriggerEnter (Collider c) 
	{
		if (c.gameObject.tag == "Player")
		{
			gem = c.gameObject;
			Destroy (this);
		}

		PlayerController.gemsTotal++;


	}
	

}
