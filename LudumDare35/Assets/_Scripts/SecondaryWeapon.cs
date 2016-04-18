using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SecondaryWeapon : MonoBehaviour {

	public enum SpecialWeapons  {
		None,
		Bunny,
		Carrot

	}

	public SpecialWeapons currentWeapon;
	private GameObject secondaryBullet;
	private SpriteRenderer spriteRender;
	private float speedBullet;
	private int randomNumber;
	private float fireRate, initialFireRate;
	private bool canShoot;
	private Animator animator;


	[SerializeField]
	private float minDistanceShooting;

	[SerializeField]
	private List<GameObject> _ammolist = new List<GameObject>();
	private List<SpecialWeapons> specialWeaponList = System.Enum.GetValues(typeof(SpecialWeapons)).Cast<SpecialWeapons>().ToList();

	[SerializeField]
	private List<Sprite> specialWeaponSprites;

	public List<SpecialWeapons> SpecialWeaponList {
		get {
			return specialWeaponList;
		}
	}

	void Start()
	{
		spriteRender = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		CurrentWeapon(SpecialWeapons.None);
		canShoot = true;
	}

	public void CurrentWeapon(SpecialWeapons current)
	{
		currentWeapon = current;

		switch(current)
		{

		case SpecialWeapons.Carrot:
		{
			gameObject.SetActive(true);
			gameObject.transform.localPosition = new Vector3(-0.023f, 0.005f, 0f);
			gameObject.transform.localScale = new Vector3(1.16f,1.04354f);
			animator.enabled = false;
			secondaryBullet = _ammolist[1];
			spriteRender.sprite = specialWeaponSprites[0];
			speedBullet = 0f;
			fireRate = 5f;
			initialFireRate = fireRate;
			break;
		}
		case SpecialWeapons.Bunny:
			gameObject.SetActive(true);
			gameObject.transform.localPosition = new Vector3(-0.01f, 0.204f, 0f);
			secondaryBullet = _ammolist[0];
			spriteRender.sprite = specialWeaponSprites[1];
			speedBullet = 1f;
			fireRate = 5f;
			initialFireRate = fireRate;
			break;
		case SpecialWeapons.None:
			gameObject.SetActive(false);
			break;

		}
	}
		

	void Update()
	{
		fireRate -= Time.deltaTime;
		if(fireRate <= 0)
		{
			canShoot = true;
			fireRate = initialFireRate;
		}
			
	}

	public void Fire(Vector3 hatPos, Vector2 shootDirection)
	{
		if(currentWeapon == SpecialWeapons.Bunny)
		{
			SetShootDirection(shootDirection);
		}

		if(canShoot)
		{
			canShoot = false;
			GameObject bulletInstance = Instantiate(secondaryBullet, new Vector3((hatPos.x + minDistanceShooting),hatPos.y, hatPos.z), Quaternion.identity) as GameObject;
			bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2((shootDirection.x + 0.1f) * speedBullet, shootDirection.y * speedBullet);
		}
	}


	void SetShootDirection(Vector2 shootDirection)
	{
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
	}

}
