using UnityEngine;
using System.Collections;

public class PickupSpawnController : MonoBehaviour {

	[SerializeField]
	private float timerSpawn;

	private bool hasSpawned;
	public GameObject pickup;

	void Start()
	{
		timerSpawn = 0f;
	}


	void Update () {
	
		timerSpawn -= Time.deltaTime;
		if(timerSpawn <= 0 && !hasSpawned)
		{
			hasSpawned = true;
			timerSpawn = 8f;
			GameObject pickupInstance = Instantiate(pickup, transform.position,Quaternion.identity) as GameObject;
			pickupInstance.GetComponent<CostumePickupScript>().SetSpawner(this.gameObject);
		}

	}

	public void SpawnStart()
	{
		hasSpawned = false;
	}
}
