using UnityEngine;
using System.Collections;

public class BunnyBehavior : MonoBehaviour {

	public float speedMovement = 2f;
	public GameObject explosion;
	public float explosionTimer;
	public int damage;

	[SerializeField]
	private bool isFacingLeft;

	private GameObject enemy;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRender;
	private Vector2 movement;
	private bool startTime;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		movement = Vector2.right;
		startTime = false;
		StartTimerExplosion();
		AudioManagerScript.Instance.PlaySoundEffect("bunnystart");
		damage = 100;

	}

	void Update()
	{
		if(startTime)
		{
			explosionTimer -= Time.deltaTime;
			if(explosionTimer <= 0)
			{
				BunnyExplode();

			}
		}


	}

	// Update is called once per frame
	void FixedUpdate () {

		Vector2 playerPos = movement * speedMovement * Time.deltaTime;
		rb.MovePosition(rb.position + playerPos);

		if(spriteRender.flipX == true && !isFacingLeft)
		{
			isFacingLeft = true;
			movement.x *= -1;
		}
		else if (spriteRender.flipX == false && isFacingLeft)
		{
			isFacingLeft = false;
			movement.x *= -1;
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{

		if(other.gameObject.CompareTag("Enemy"))
		{
				enemy = other.gameObject;
				enemy.GetComponent<EnemyController>().CurrentHealth -=  damage;
				AudioManagerScript.Instance.PlaySoundEffect("bunnyend");
				GameObject bunnyExplosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
				Destroy(gameObject);
		}
	
	}

	void StartTimerExplosion()
	{
		explosionTimer = 3f;
		startTime = true;
	}
		
	void BunnyExplode()
	{
		AudioManagerScript.Instance.PlaySoundEffect("bunnyend");
		GameObject bunnyExplosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		Destroy(gameObject);
	}
		
}
