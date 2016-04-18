using UnityEngine;
using System.Collections;

public class Randomize : MonoBehaviour {

	public int lastSelected;
	public int randomInt;

	public int RandomInt {
		get {
			return randomInt;
		}
		set {
			randomInt = value;
		}
	}

	public int LastSelected {
		get {
			return lastSelected;
		}
		set {
			lastSelected = value;
		}
	}

	void Awake()
	{
		lastSelected = -1;
		randomInt = -1;
	}

	public float RandomTime(float minValue, float maxValue)
	{
		return Random.Range(minValue, maxValue);
	}
		
}
