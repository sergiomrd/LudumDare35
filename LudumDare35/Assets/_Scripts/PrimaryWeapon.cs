using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrimaryWeapon : MonoBehaviour {

	public enum NormalWeapons  {
		Moustache,
		Arms
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
		case NormalWeapons.Arms:
			primaryBullet = _ammolist[1];
			speedBullet = 1f;
			fireRate = 0f;
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

	public void Smash(Vector3 facePos, bool isFacingLeft)
	{
		if(canShoot)
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
