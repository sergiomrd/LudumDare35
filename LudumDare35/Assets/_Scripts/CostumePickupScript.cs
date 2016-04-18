using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CostumePickupScript : MonoBehaviour {

	public GameObject face, hat;

	private PrimaryWeapon primaryWeapon;
	private SecondaryWeapon secondaryWeapon;
	public int random;
	public int lastSelected;



	void Start()
	{
		primaryWeapon = face.GetComponent<PrimaryWeapon>();
		secondaryWeapon = hat.GetComponent<SecondaryWeapon>();
		random = GameManagerScript.Instance.GetComponent<Randomize>().RandomInt;
		lastSelected = GameManagerScript.Instance.GetComponent<Randomize>().LastSelected;

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
		GameManagerScript.Instance.GetComponent<Randomize>().RandomInt = random;

		if(GameManagerScript.Instance.GetComponent<Randomize>().RandomInt == GameManagerScript.Instance.GetComponent<Randomize>().LastSelected)
		{
			while(GameManagerScript.Instance.GetComponent<Randomize>().RandomInt == GameManagerScript.Instance.GetComponent<Randomize>().LastSelected)
			{
				random = Random.Range(1, primaryWeapon.NormalWeaponList.Count);
				GameManagerScript.Instance.GetComponent<Randomize>().RandomInt = random;
			}

			primaryWeapon.CurrentWeapon(primaryWeapon.NormalWeaponList[random]);
			lastSelected = random;
			GameManagerScript.Instance.GetComponent<Randomize>().LastSelected = lastSelected;

		}
		else 
		{
			
			primaryWeapon.CurrentWeapon(primaryWeapon.NormalWeaponList[random]);
			lastSelected = random;
			GameManagerScript.Instance.GetComponent<Randomize>().LastSelected = lastSelected;
		}
			

	}

	void Test()
	{
		
	}
}
