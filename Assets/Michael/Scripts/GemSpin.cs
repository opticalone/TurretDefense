using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpin : MonoBehaviour {

	public float rotationSpeed = 3.0f; 



	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0f, rotationSpeed, 0f));

	}
}
