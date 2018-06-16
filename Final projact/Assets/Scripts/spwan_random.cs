using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spwan_random : MonoBehaviour {

	public GameObject[] thePowerUps;
	GameObject powerUp;
	public Transform m;

	
	// Use this for initialization
	void Start () {

		m = GetComponent<Transform> ();
		int rand = (int)(Mathf.Floor (Random.Range (0, thePowerUps.Length)));
		Instantiate (thePowerUps[rand],m.position, m.rotation);

	}

}
