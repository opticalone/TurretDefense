using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour 
{

	[Header("c o l o r s")]

	private Color StartColor;
	public Color HoverColor;

	private GameObject turret;

	private Renderer rend;

	void Start ()
		{
		rend = GetComponent<Renderer> ();
		StartColor = rend.material.color;
		}

	void OnMouseDown()
	{
		if (turret != null) 
		{
		
			Debug.Log ("Can't build there");
			return;
		}
		//build a turret
		GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
		turret =(GameObject)Instantiate (turretToBuild, transform.position, transform.rotation);
	}



	void OnMouseEnter()
	{
		rend.material.color = HoverColor;

	}
	void OnMouseExit()
	{
		rend.material.color = StartColor;
	}
}
