using UnityEngine;
using System.Collections;

public class ArmsBehavior : MonoBehaviour {

	public float damage;
	private Rigidbody2D rb;
	private float timer;

	private AudioSource audioSource;


	[SerializeField]
	private GameObject enemy;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		gameObject.GetComponent<SpriteRenderer>().sprite = null;
		gameObject.GetComponent<Animator>().enabled = false;
		audioSource = GetComponent<AudioSource>();
		timer = 0.1f;
		damage = 60;

	}

	void Update () {

		timer -= Time.deltaTime;
		if(timer < 0f)
		{
			Destroy(gameObject);
			timer = 0.1f;
		}
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			AudioManagerScript.Instance.PlaySoundEffect("punch");
			enemy = other.gameObject;
			enemy.GetComponent<EnemyController>().CurrentHealth -=  damage;
			Destroy(gameObject);


		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			AudioManagerScript.Instance.PlaySoundEffect("punch");
			enemy = other.gameObject;
			enemy.GetComponent<EnemyController>().CurrentHealth -=  damage;
			Destroy(gameObject);

		}
	}
}
