using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	public Button buttonStart;
	public Button buttonQuit;

	GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();

		if (gm) {
			if (buttonStart) {
				buttonStart.onClick.AddListener (gm.StartGame);
			}
			if (buttonQuit) {
				buttonQuit.onClick.AddListener (gm.QuitGame);
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
