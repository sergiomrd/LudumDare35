using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CostumePickupScript : MonoBehaviour {

	public GameObject face, hat;

	private PrimaryWeapon primaryWeapon;
	private SecondaryWeapon secondaryWeapon;
	private int random;



	void Start()
	{
		primaryWeapon = face.GetComponent<PrimaryWeapon>();
		secondaryWeapon = hat.GetComponent<SecondaryWeapon>();

	}

	void Update () {
	

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Player"))
		{
			SelectCostume();
			Destroy(gameObject);

		}
	}

	void SelectCostume()
	{
		random = Random.Range(1, primaryWeapon.NormalWeaponList.Count);
		primaryWeapon.CurrentWeapon(primaryWeapon.NormalWeaponList[random]);
		Debug.Log(primaryWeapon.NormalWeaponList[1]);
	}

	void Test()
	{
		
	}
}
