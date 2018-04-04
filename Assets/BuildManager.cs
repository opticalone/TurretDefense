using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour 
{
	//Singleton patter (only one build manager, accessable)

	public static BuildManager instance;

	void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("MORE THAN ONE BUILD MANAGER IN SCENE");
			return;
		}
		instance = this;

	}
	//end of singleton

	[Header("t u r r e t s")]

	public GameObject StandardTurretPrefab;

	void Start()
	{
		turretToBuild = StandardTurretPrefab;
	}

	private GameObject turretToBuild;

	//public GameObject GetTurretToBuild()
	//{
	//	return GetTurretToBuild;
	//}
}
