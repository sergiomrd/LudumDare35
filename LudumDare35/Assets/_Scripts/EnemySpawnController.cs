using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour {

	[SerializeField]
	private bool setDirectionLeft;

	[SerializeField]
	private float timerSpawn;

	private bool hasSpawned;
	public GameObject enemy;

	void Start () {

		timerSpawn = 0f;
	}

	void Update () {

		timerSpawn -= Time.deltaTime;
		if(timerSpawn <= 0)
		{
			timerSpawn = Random.Range(3f,5f);
			GameObject enemyInstance = Instantiate(enemy, transform.position ,Quaternion.identity) as GameObject;
			enemyInstance.GetComponent<EnemyController>().SetSpawner(this.gameObject, setDirectionLeft);
		}
	
	}

}
