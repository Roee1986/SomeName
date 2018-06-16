using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	Transform playerT; 
	Vector3 offset;

	// Use this for initialization
	void Start () {
		playerT = GameObject.FindGameObjectWithTag ("Player").transform;
		offset = this.transform.position - playerT.position;
	}
	
	// Update is called once per frame
	void Update () {

		this.transform.position = playerT.position + offset;
	}
}
