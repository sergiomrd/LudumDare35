using UnityEngine;
using System.Collections;

public class ArmsBehavior : MonoBehaviour {

	private Rigidbody2D rb;
	private float timer;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		timer = 0.1f;

	}

	void Update () {

		timer -= Time.deltaTime;
		if(timer < 0.1f)
		{
			Destroy(gameObject);
			timer = 0.1f;
		}
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
