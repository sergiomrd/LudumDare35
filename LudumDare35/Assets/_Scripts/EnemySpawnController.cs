using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	[SerializeField]
	private bool setDirectionLeft;

	[SerializeField]
	private float timerSpawn;
	private float minVal, maxVal;
	private float spawnSpeed;

	private bool hasSpawned;
	public GameObject enemy;

	void Start () {

		timerSpawn = 0f;
		minVal = 5f;
		maxVal = 7f;
		spawnSpeed = 2f;
	}

	void Update () {

		timerSpawn -= Time.deltaTime;
		if(timerSpawn <= 0)
		{
			timerSpawn = Random.Range(minVal,maxVal);
			GameObject enemyInstance = Instantiate(enemy, transform.position ,Quaternion.identity) as GameObject;
			enemyInstance.GetComponent<EnemyController>().SetSpawner(this.gameObject, setDirectionLeft);
			enemyInstance.GetComponent<EnemyController>().speedMovement = spawnSpeed;
		}

		if(Time.time > 10)
		{
			maxVal = 5f;
			minVal = 3f;
			spawnSpeed = 3f;
		}
		else if( Time.time > 20)
		{
			maxVal = 3f;
			minVal = 1f;
			spawnSpeed = 4f;
		}
		else if( Time.time > 20)
		{
			maxVal = 1f;
			minVal = 0f;
			spawnSpeed = 5f;
		}


	
	}

}
