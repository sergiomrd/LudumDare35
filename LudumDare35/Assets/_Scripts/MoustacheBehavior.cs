using UnityEngine;
using System.Collections;

public class MoustacheBehavior : MonoBehaviour {


	public float damage;

	private Rigidbody2D rb;
	private float timer;

	[SerializeField]
	private GameObject enemy;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		timer = 2f;
		damage = 40;

	}

	void Update () {
		
		timer -= Time.deltaTime;
		if(timer <= 0f)
		{
			Destroy(gameObject);
			timer = 2f;
		}


	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Destroy(gameObject);
			enemy = other.gameObject;
			enemy.GetComponent<EnemyController>().CurrentHealth -=  damage;


		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Destroy(gameObject);
			enemy = other.gameObject;
			enemy.GetComponent<EnemyController>().CurrentHealth -=  damage;

		}
	}
}
