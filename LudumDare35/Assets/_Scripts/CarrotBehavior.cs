using UnityEngine;
using System.Collections;

public class CarrotBehavior : MonoBehaviour {

	public GameObject explosion;
	public int damage = 100;

	private GameObject enemy;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		AudioManagerScript.Instance.PlaySoundEffect("carrot");
		damage = 100;

	}

	void OnCollisionEnter2D(Collision2D other)
	{

		if(other.gameObject.CompareTag("Enemy"))
		{
			enemy = other.gameObject;
			enemy.GetComponent<EnemyController>().CurrentHealth -=  damage;
			AudioManagerScript.Instance.StopLoop("carrot");
			AudioManagerScript.Instance.PlaySoundEffect("bunnyend");
			GameObject bunnyExplosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
			Destroy(gameObject);
		}

	}

}

