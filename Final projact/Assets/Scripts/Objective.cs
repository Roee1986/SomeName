using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

	public int numberOfCollectibles;
	public GameObject[] allCollectibles;
	public GameObject finishLine;
	public GameObject player;
	// Use this for initialization
	void Start () {

		allCollectibles = GameObject.FindGameObjectsWithTag ("Collectable");
		numberOfCollectibles = allCollectibles.Length;

		if (numberOfCollectibles <= 0) {
		
			Debug.Log ("no Collectible found!!!");
		}
		finishLine = GameObject.FindGameObjectWithTag ("FinnishLine");
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		if (player && finishLine) {
		
			float distanceToFinish = Vector2.Distance (player.transform.position
				, finishLine.transform.position);

			//Debug.Log (distanceToFinish);
		}
	}
}
