using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManagerScript : MonoBehaviour {

	public static AudioManagerScript Instance { get; private set; }

	private AudioSource audioSourceMusic;
	private AudioSource audioSourceSfx;
	public GameObject sfx;

	[SerializeField]
	private List<AudioClip> SoundEffects = new List<AudioClip>();
	[SerializeField]
	private List<AudioClip> Music = new List<AudioClip>();


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

	}

	// Use this for initialization
	void Start () {
	
		audioSourceMusic = GetComponent<AudioSource>();
		audioSourceMusic.clip = Music[Random.Range(0, Music.Count)];
		audioSourceMusic.Play();
		audioSourceSfx = sfx.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySoundEffect(string clip)
	{
		switch(clip.ToLower())
		{
		case "slowmo":
			audioSourceSfx.clip = SoundEffects[0];
			break;
		case "punch":
			audioSourceSfx.clip = SoundEffects[3];
			break;
		case "bunnystart":
			audioSourceSfx.clip = SoundEffects[2];
			break;
		case "bunnyend":
			audioSourceSfx.clip = SoundEffects[1];
			break;

		case "crow":
			audioSourceSfx.clip = SoundEffects[4];
			break;
		}

		audioSourceSfx.Play();


	}
}
