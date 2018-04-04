using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	public float timeBetweenEnemies = 5f;
	public float countdown = 2f;

	public Text waveCountdownText;

	private int waveIdx = 1;



	void Update ()
	{
		if (countdown <= 0f) 
		{
			StartCoroutine (spawnWave());
			countdown = timeBetweenWaves;
		}
		countdown -= Time.deltaTime;
		waveCountdownText.text = Mathf.Round(countdown).ToString ();
	}

	IEnumerator spawnWave()
	{

		int wavenum = 1;

		for (int i = 0; i < waveIdx; i++) {

			SpawnEnemy ();
			yield return new WaitForSeconds (timeBetweenEnemies);
			wavenum++;
		}
			
		waveIdx++;
		Debug.Log("wave time" +wavenum);
	}

	void SpawnEnemy(){
	
		Instantiate (enemyPrefab, spawnPoint.position, spawnPoint.rotation);
	}


}
