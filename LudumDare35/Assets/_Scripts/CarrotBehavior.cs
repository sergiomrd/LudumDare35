using UnityEngine;
using System.Collections;

public class CarrotBehavior : MonoBehaviour {

	public GameObject explosion;


	private Rigidbody2D rb;
	private SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		AudioManagerScript.Instance.PlaySoundEffect("carrot");

	}

	void OnCollisionEnter2D(Collision2D other)
	{

		if(other.gameObject.CompareTag("Enemy"))
		{
			AudioManagerScript.Instance.StopLoop("carrot");
			AudioManagerScript.Instance.PlaySoundEffect("bunnyend");
			GameObject bunnyExplosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
			Destroy(other.gameObject);
			Destroy(gameObject);
		}

	}

}

