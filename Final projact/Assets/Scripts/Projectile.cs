using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


	public float speed;
	public float lifeTime;
	public float bounceForce;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		if (!rb) {

			Debug.LogWarning ("Rigidbody2D not found on" + name);
		}

		if (lifeTime <= 0) {
			lifeTime = 2.0f;

			Debug.LogWarning ("lifeTime not set on" + name);
		}
		bounceForce = 0.05f;

		Destroy (gameObject, lifeTime);
	}
	// make projectiles bounce of floor and destroy them
	void OnCollisionEnter2D(Collision2D c){
		rb.velocity = new Vector2 (rb.velocity.x, 0);
		rb.AddForce (Vector2.up * bounceForce, ForceMode2D.Impulse);
		Debug.LogWarning (c.gameObject.tag);
		if (c.gameObject.tag == "destroyProj") {
			Destroy (gameObject);
			Debug.LogWarning ("destroyProjectile");
		
		}
	}
		
}
