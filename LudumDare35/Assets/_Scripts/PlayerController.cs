using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speedMovement = 1f;
	public GameObject hat, face, primaryBullet, secondaryBullet;
	public float jumpPower = 150f;
	public float speedBullet = 10f;

	private Rigidbody2D rb;
	private bool isFacingLeft;
	private SpriteRenderer spriteRender;
	private bool isJumping;
	private bool doubleJump;
	private float minDistanceShooting;

	void Start () {

		rb = GetComponent<Rigidbody2D>();
		spriteRender = GetComponent<SpriteRenderer>();
		isFacingLeft = false;
		isJumping = false;
		doubleJump = false;
		minDistanceShooting = 0.3f;
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
	}

	public void FireWeapon()
	{
		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.z = 0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;

		GameObject bulletInstance = Instantiate(primaryBullet, face.transform.position, Quaternion.identity) as GameObject;
		bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speedBullet, shootDirection.y * speedBullet);

	}

	public void FireHat()
	{
		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.z = 0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;

		if(shootDirection.x > 0)
		{
			secondaryBullet.GetComponent<SpriteRenderer>().flipX = false;
			minDistanceShooting = Mathf.Abs(minDistanceShooting);
		}
		else if (shootDirection.x < 0)
		{
			secondaryBullet.GetComponent<SpriteRenderer>().flipX = true;
			if(minDistanceShooting > 0)
			{
				minDistanceShooting = -minDistanceShooting;
			}

		}
			
		GameObject bulletInstance = Instantiate(secondaryBullet, new Vector3((hat.transform.position.x + minDistanceShooting), hat.transform.position.y, hat.transform.position.z), Quaternion.identity) as GameObject;
		bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2((shootDirection.x + 0.1f) * speedBullet, shootDirection.y * speedBullet);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Ground"))
		{
			isJumping = false;
			doubleJump = true;
		}
	}
}
