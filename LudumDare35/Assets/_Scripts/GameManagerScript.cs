using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript Instance { get; private set; }
	public Text deadTimesText;
	public Text numKilledText;

	[SerializeField]
	private float deadTimes = 0;

	private float dumbKilled;

	public float DumbKilled {
		get {
			return dumbKilled;
		}
		set {
			dumbKilled = value;
			numKilledText.text = "NUMBER OF DUMBFACES MOUSTACHED: " + dumbKilled;
		}
	}

	void Awake()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}

		DontDestroyOnLoad(this);
			
	}
		
	void Start () {
		deadTimesText.text = "NUMBER OF AWESOME MOUSTACHES LOSED: " + deadTimes;
		numKilledText.text = "NUMBER OF DUMBFACES MOUSTACHED: " + dumbKilled;
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void SlowMotion()
	{
		Time.timeScale = 0.5f;
	}

	public void RestartGame()
	{
		deadTimes++;
		deadTimesText.text = "NUMBER OF AWESOME MOUSTACHES LOSED: " + deadTimes;
		DumbKilled = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}
}
