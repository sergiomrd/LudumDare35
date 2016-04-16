using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speedMovement = 1f;
	public GameObject Hat, Face, Bullet;
	public float jumpPower = 150f;
	public float speedBullet = 10f;

	private Rigidbody2D rb;
	private bool isFacingLeft;
	private SpriteRenderer spriteRender;
	private bool isJumping;
	private bool doubleJump;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		isFacingLeft = false;
		isJumping = false;
		doubleJump = false;
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

		Hat.GetComponent<SpriteRenderer>().flipX = (Hat.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		Hat.transform.localRotation = Quaternion.Euler(new Vector3(Hat.transform.localEulerAngles.x, Hat.transform.localEulerAngles.y, -Hat.transform.localEulerAngles.z));
		Hat.transform.localPosition = new Vector3(-Hat.transform.localPosition.x, Hat.transform.localPosition.y, Hat.transform.localPosition.z);

		//Flips the player
		spriteRender.flipX = (spriteRender.flipX == false) ?  true : false;

		//Flips the Face
		Face.GetComponent<SpriteRenderer>().flipX = (Face.GetComponent<SpriteRenderer>().flipX == false) ? true : false;
		Face.transform.localPosition = new Vector3(-Face.transform.localPosition.x, Face.transform.localPosition.y, Face.transform.localPosition.z);
	}

	public void FireWeapon()
	{
		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.z = 0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;

		GameObject bulletInstance = Instantiate(Bullet, Face.transform.position, Quaternion.identity) as GameObject;
		bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speedBullet, shootDirection.y * speedBullet);

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Ground"))
		{
			Debug.Log("Enter");
			isJumping = false;
			doubleJump = true;
		}
	}
}
