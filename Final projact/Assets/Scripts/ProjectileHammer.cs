using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHammer : MonoBehaviour {

	public float speed;
	public float lifeTime;

	// Use this for initialization
	void Start () {

		if (lifeTime <= 0) {
			lifeTime = 2.0f;
			Debug.LogError("LifeTime not set on " + name + ". Defaulting to " + lifeTime);
		}
		if (speed <= 0) {
			//speed = -15.0f;
			Debug.LogError("speed not set on " + name + ". Defaulting to " + speed);
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		Destroy(gameObject, lifeTime);
	}

	void OnCollisionEnter2D(Collision2D col)
	{ Destroy(gameObject); }
}
