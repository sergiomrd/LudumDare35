using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript Instance { get; private set; }

	void Awake()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
			
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
