using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PrimaryWeapon : MonoBehaviour {

	public enum NormalWeapons {
		None,
		Moustache,
		Arms

	}

	public NormalWeapons currentWeapon;
	private GameObject primaryBullet;
	public GameObject decorative;
	private SpriteRenderer spriteRender;
	private float speedBullet;
	private bool canShoot;
	private int randomNumber;
	private float fireRate, initialFireRate;
	private Animator animator;

	[SerializeField]
	private List<GameObject> _ammolist = new List<GameObject>();
	private List<NormalWeapons> normalWeaponList = System.Enum.GetValues(typeof(NormalWeapons)).Cast<NormalWeapons>().ToList();

	[SerializeField]
	private List<Sprite> normalWeaponSprites;

	public List<NormalWeapons> NormalWeaponList {
		get {
			return normalWeaponList;
		}
	}

	void Start()
	{
		spriteRender = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		CurrentWeapon(NormalWeapons.None);
		canShoot = true;

	}

	void Update () {

		fireRate -= Time.deltaTime;
		if(fireRate <= 0)
		{
			canShoot = true;
			fireRate = initialFireRate;
		}
			
	}

	public void CurrentWeapon(NormalWeapons current)
	{
		currentWeapon = current;

		switch(current)
		{
		case NormalWeapons.Moustache:
			gameObject.SetActive(true);
			decorative.SetActive(false);
			animator.enabled = false;
			primaryBullet = _ammolist[0];
			spriteRender.sprite = normalWeaponSprites[0];
			speedBullet = 1f;
			fireRate = 0.5f;
			initialFireRate = fireRate;
			break;
		case NormalWeapons.Arms:
			gameObject.SetActive(true);
			decorative.SetActive(true);
			animator.enabled = true;
			primaryBullet = _ammolist[1];
			spriteRender.sprite = normalWeaponSprites[1];
			speedBullet = 2f;
			fireRate = 0f;
			initialFireRate = fireRate;
			break;
		case NormalWeapons.None:
			gameObject.SetActive(false);
			decorative.SetActive(false);
			break;
		}
	}

	public void Fire(Vector3 facePos, Vector2 shootDirection)
	{
		if(canShoot && currentWeapon != NormalWeapons.None)
		{
			canShoot = false;
			GameObject bulletInstance = Instantiate(primaryBullet, facePos, Quaternion.identity) as GameObject;
			bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speedBullet, shootDirection.y * speedBullet);
		}

	}

	public void Smash(Vector3 facePos, bool isFacingLeft)
	{
		if(canShoot && currentWeapon != NormalWeapons.None)
		{
			canShoot = false;
			setSmashDirection(isFacingLeft);
			GameObject bulletInstance = Instantiate(primaryBullet, facePos, Quaternion.identity) as GameObject;
			bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(((isFacingLeft) ? Vector2.left.x : Vector2.right.x) * speedBullet, 0);
		}
	}

	void setSmashDirection(bool isFacinLeft)
	{
		if(isFacinLeft)
		{
			primaryBullet.GetComponent<SpriteRenderer>().flipX = true;
		}

		else
		{
			primaryBullet.GetComponent<SpriteRenderer>().flipX = false;
		}
	}
}
