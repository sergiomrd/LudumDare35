using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecondaryWeapon : MonoBehaviour {

	public enum SpecialWeapons  {
		Bunny,
		None
	}

	public SpecialWeapons currentWeapon;
	private float fireRate;
	private GameObject secondaryBullet;
	private float speedBullet;
	private float initialFireRate;
	private bool canShoot;


	[SerializeField]
	private float minDistanceShooting;

	[SerializeField]
	private List<GameObject> _ammolist = new List<GameObject>();

	public void CurrentWeapon(SpecialWeapons current)
	{
		currentWeapon = current;

		switch(current)
		{
		case SpecialWeapons.Bunny:
			secondaryBullet = _ammolist[0];
			speedBullet = 1f;
			fireRate = 5f;
			initialFireRate = fireRate;
			break;
		case SpecialWeapons.None:
			gameObject.SetActive(false);
			break;

		}
	}

	void Start()
	{
		CurrentWeapon(currentWeapon);
		canShoot = true;
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
