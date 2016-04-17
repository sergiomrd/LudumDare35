using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrimaryWeapon : MonoBehaviour {

	public enum NormalWeapons  {
		Moustache
	}

	public NormalWeapons currentWeapon;
	private GameObject primaryBullet;
	private float speedBullet;
	private bool canShoot;
	private float fireRate, initialFireRate;

	[SerializeField]
	private List<GameObject> _ammolist = new List<GameObject>();

	void Start()
	{
		CurrentWeapon(currentWeapon);
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
			primaryBullet = _ammolist[0];
			speedBullet = 1f;
			fireRate = 0.5f;
			initialFireRate = fireRate;
			break;

		}
	}

	public void Fire(Vector3 facePos, Vector2 shootDirection)
	{
		if(canShoot)
		{
			canShoot = false;
			GameObject bulletInstance = Instantiate(primaryBullet, facePos, Quaternion.identity) as GameObject;
			bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * speedBullet, shootDirection.y * speedBullet);
		}

	}
}
