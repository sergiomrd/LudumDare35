using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speedMovement = 2f;

	public GameObject hat, face;
	public GameObject healthBar;

	[SerializeField]
	private GameObject spawner;

	[SerializeField]
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
		hat = transform.FindChild("Hat").gameObject;
		face = transform.FindChild("Face").gameObject;
		enemyHealth = healthBar.gameObject.GetComponentInChildren<HealthBar>();
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

		hat.GetComponent<SpriteRenderer>().flipX = (hat.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		hat.transform.localRotation = Quaternion.Euler(new Vector3(hat.transform.localEulerAngles.x, hat.transform.localEulerAngles.y, -hat.transform.localEulerAngles.z));
		hat.transform.localPosition = new Vector3(-hat.transform.localPosition.x, hat.transform.localPosition.y, hat.transform.localPosition.z);

		//Flips the player
		spriteRender = GetComponent<SpriteRenderer>();
		spriteRender.flipX = (spriteRender.flipX == false) ?  true : false;

		//Flips the Face
		face.GetComponent<SpriteRenderer>().flipX = (face.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		face.transform.localPosition = new Vector3(-face.transform.localPosition.x, face.transform.localPosition.y, face.transform.localPosition.z);
	}
		
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Wall"))
		{
			FlipEnemy();
		}
		if(col.gameObject.CompareTag("Enemy"))
		{
			FlipEnemy();
		}

	}

	void DeadEnemy()
	{
		GameManagerScript.Instance.DumbKilled++;
		Destroy(gameObject);
	}

	public void SetSpawner(GameObject spawner, bool facingLeft)
	{
		this.spawner = spawner;

		if(facingLeft)
		{
			FlipEnemy();
			movement = Vector2.left;

		}
		else
		{
			movement = Vector2.right;
		
		}
	}
}
