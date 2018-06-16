using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	
	static SoundManager _instance = null;

	public AudioSource musicSource;
	public AudioSource sfxSource;


	// Use this for initialization
	void Start () {
		if (instance)
			Destroy (gameObject);
		else {
		
			instance = this;
			DontDestroyOnLoad (this);
		}
	}
	
	// Update is called once per frame

	public void playSingleSound(AudioClip clip, float volume=1.0f , bool loop = false){

		sfxSource.clip = clip;

		sfxSource.volume = volume;

		sfxSource.Play ();

		sfxSource.loop = loop;

	}
	public void playSingleMusic(AudioClip clip, float volume=1.0f , bool loop = true, bool stop = false){

		musicSource.clip = clip;

		musicSource.volume = volume;

		musicSource.Play ();

		musicSource.loop = loop;

		if(stop)
			musicSource.Stop();

	}

	public static SoundManager instance {

		get { return _instance; }
		set { _instance = value;
			Debug.Log ("instance changed to: " + _instance);
		}
	}
}
