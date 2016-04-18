using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speedMovement = 2f;
	public GameObject Hat, Face;

	public GameObject healthBar;
	private HealthBar enemyHealth;
	private Rigidbody2D rb;
	private SpriteRenderer spriteRender;
	private Vector2 movement;

	[SerializeField]
	private float currentHealth;

	[SerializeField]
	private float maxHealth;

	[SerializeField]
	private bool isFacingLeft;

	public bool IsFacingLeft {
		get {
			return isFacingLeft;
		}
	}

	public float CurrentHealth {
		get {
			return currentHealth;
		}
		set {
			currentHealth = value;
			enemyHealth.Value = currentHealth;
		}
	}

	public float MaxHealth {
		get {
			return maxHealth;
		}
		set {
			maxHealth = value;
			enemyHealth.MaxValue = maxHealth;
		}
	}

	void Start () {
	
		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		isFacingLeft = false;
		FlipEnemy();
		movement = new Vector2(-1,0);
		enemyHealth = healthBar.GetComponent<HealthBar>();
		MaxHealth = 100;
		currentHealth = MaxHealth;
	}

	void Update()
	{
		if(CurrentHealth <= 0)
		{
			DeadEnemy();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 playerPos = movement * speedMovement * Time.deltaTime;
		rb.MovePosition(rb.position + playerPos);
			
	}

	public void FlipEnemy()
	{

		isFacingLeft = !isFacingLeft;
		movement *= -1;
		//Flips the Hat

		Hat.GetComponent<SpriteRenderer>().flipX = (Hat.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		Hat.transform.localRotation = Quaternion.Euler(new Vector3(Hat.transform.localEulerAngles.x, Hat.transform.localEulerAngles.y, -Hat.transform.localEulerAngles.z));
		Hat.transform.localPosition = new Vector3(-Hat.transform.localPosition.x, Hat.transform.localPosition.y, Hat.transform.localPosition.z);

		//Flips the player
		spriteRender.flipX = (spriteRender.flipX == false) ?  true : false;

		//Flips the Face
		Face.GetComponent<SpriteRenderer>().flipX = (Face.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		Face.transform.localPosition = new Vector3(-Face.transform.localPosition.x, Face.transform.localPosition.y, Face.transform.localPosition.z);
	}
		
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Wall"))
		{
			FlipEnemy();
		}

	}

	void DeadEnemy()
	{
		Destroy(gameObject);
	}
}
