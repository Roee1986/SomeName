using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	Transform target;

	public Transform cameraBoundMin;
	public Transform cameraBoundMax;

	float xMin, xMax, yMin, yMax;

	// Use this for initialization
	void Start () {
		GameObject g = GameObject.FindGameObjectWithTag ("Player");
		if (!g) {
		
			Debug.Log ("Player not found");
			return;
		}
		target = g.GetComponent<Transform> ();

		xMin = cameraBoundMin.position.x;
		yMin = cameraBoundMin.position.y;

		xMax = cameraBoundMax.position.x;
		yMax = cameraBoundMax.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
			transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax),
				Mathf.Clamp (target.position.y, yMin, yMax),
				transform.position.z);
		} 
		else {
			GameObject g = GameObject.FindGameObjectWithTag ("Player");
			target = g.GetComponent<Transform> ();
		}
	}
}
