using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

	public int spawnLocation;
	public AudioClip BGMusic;

	void Awake () {
		SoundManager.instance.playSingleMusic(BGMusic,0.1f);
		if (spawnLocation < 0) {
			spawnLocation = 0; 
		}

		GameManager.instance.spawnPlayer (spawnLocation);

		GameManager.instance.scoreText = GameObject.Find ("Score_Text").GetComponent<Text> ();

		GameManager.instance.score = 0;

		GameManager.instance.canvas = GameObject.Find ("Pause_Screen").GetComponent<CanvasGroup> ();
	}
	

}
