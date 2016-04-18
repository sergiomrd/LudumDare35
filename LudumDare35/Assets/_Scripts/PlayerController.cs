using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speedMovement = 1f;
	public GameObject hat, face, decorative;
	public float jumpPower = 150f;

	public GameObject Hat {
		get {
			return hat;
		}
	}

	public GameObject Face {
		get {
			return face;
		}
	}

	private PrimaryWeapon primaryWeapon;
	public GameObject healthBar;
	private HealthBar playerHealth;
	private SecondaryWeapon secondaryWeapon;
	private Rigidbody2D rb;
	private bool isFacingLeft;
	private SpriteRenderer spriteRender;
	private bool isJumping;
	private bool doubleJump;
	private Animator faceAnimator;

	[SerializeField]
	private float currentHealth;

	[SerializeField]
	private float maxHealth;

	public float CurrentHealth {
		get {
			return currentHealth;
		}
		set {
			currentHealth = value;
			playerHealth.Value = currentHealth;
		}
	}

	public float MaxHealth {
		get {
			return maxHealth;
		}
		set {
			maxHealth = value;
			playerHealth.MaxValue = maxHealth;
		}
	}

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		primaryWeapon = face.GetComponent<PrimaryWeapon>();
		secondaryWeapon = hat.GetComponent<SecondaryWeapon>();
		isFacingLeft = false;
		isJumping = false;
		doubleJump = false;
		faceAnimator = face.GetComponent<Animator>();
		playerHealth = healthBar.GetComponent<HealthBar>();
		MaxHealth = 100;
		CurrentHealth = MaxHealth;
	}


	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(isJumping == false)
			{
				rb.AddForce(Vector2.up * jumpPower);
				isJumping = true;
			}
			else if (isJumping == true && doubleJump)
			{
				rb.AddForce(Vector2.up * jumpPower);
				isJumping = true;
				doubleJump = false;
			}

		}

		if(Input.GetButtonDown("Fire1"))
		{
			FireWeapon();
		}

		if(Input.GetButtonDown("Fire2"))
		{
			FireHat();
		}

		if(CurrentHealth <= 0)
		{
			Dead();
		}

	}

	// Update is called once per frame
	void FixedUpdate () {
	
		Vector2 movement = new Vector2 (Input.GetAxisRaw("Horizontal"), 0);
		Vector2 playerPos = movement * speedMovement * Time.deltaTime;
		rb.MovePosition(rb.position + playerPos);

		if(movement.x < 0 && !isFacingLeft)
		{
			
			FlipPlayer();
		}
		else if(movement.x > 0 && isFacingLeft)
		{
			FlipPlayer();
		}



	}

	public void FlipPlayer()
	{
		isFacingLeft = !isFacingLeft;

		//Flips the Hat

		hat.GetComponent<SpriteRenderer>().flipX = (hat.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		hat.transform.localRotation = Quaternion.Euler(new Vector3(hat.transform.localEulerAngles.x, hat.transform.localEulerAngles.y, -hat.transform.localEulerAngles.z));
		hat.transform.localPosition = new Vector3(-hat.transform.localPosition.x, hat.transform.localPosition.y, hat.transform.localPosition.z);

		//Flips the player
		spriteRender.flipX = (spriteRender.flipX == false) ?  true : false;

		//Flips the Face
		face.GetComponent<SpriteRenderer>().flipX = (face.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		face.transform.localPosition = new Vector3(-face.transform.localPosition.x, face.transform.localPosition.y, face.transform.localPosition.z);

		decorative.GetComponent<SpriteRenderer>().flipX = (decorative.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		decorative.transform.localPosition = new Vector3(-decorative.transform.localPosition.x, decorative.transform.localPosition.y, decorative.transform.localPosition.z);
	}

	public void FireWeapon()
	{
		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.z = 0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;

		if(primaryWeapon.currentWeapon != PrimaryWeapon.NormalWeapons.Arms)
		{
			primaryWeapon.Fire(face.transform.position, shootDirection);
		}
		else
		{
			primaryWeapon.Smash(face.transform.position, isFacingLeft);
			faceAnimator.SetTrigger("smash");

		}



	}

	public void FireHat()
	{

		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.z = 0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;

		secondaryWeapon.Fire(hat.transform.position, shootDirection);

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Ground"))
		{
			isJumping = false;
			doubleJump = true;
		}

		if(col.gameObject.CompareTag("Enemy"))
		{
			Debug.Log(col.gameObject.GetComponent<EnemyController>().IsFacingLeft);

			if(col.gameObject.GetComponent<EnemyController>().IsFacingLeft && !isFacingLeft)
			{
				rb.AddForce(Vector2.left * jumpPower);
				CurrentHealth -= 20;
			}
			else if(col.gameObject.GetComponent<EnemyController>().IsFacingLeft && isFacingLeft)
			{
				rb.AddForce(Vector2.left * jumpPower);
				CurrentHealth -= 20;
			}
			else if(!col.gameObject.GetComponent<EnemyController>().IsFacingLeft && isFacingLeft)
			{
				rb.AddForce(Vector2.right * jumpPower);
				CurrentHealth -= 20;
			}
			else if(!col.gameObject.GetComponent<EnemyController>().IsFacingLeft && !isFacingLeft)
			{
				rb.AddForce(Vector2.right * jumpPower);
				CurrentHealth -= 20;
			}
				
		}
	}

	void Dead()
	{
			
	}
}
