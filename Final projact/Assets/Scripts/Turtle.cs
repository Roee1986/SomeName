using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Turtle : MonoBehaviour {

	public AudioClip killSFX;

	Rigidbody2D rb;
	public float speed;
	public bool isFacingLeft;

	// Use this for initialization
	void Start () {
		
		isFacingLeft = true;
		rb = GetComponent<Rigidbody2D> ();
		//if (!rb) { not nessesery becouse [RequireComponent(typeof(Rigidbody2D))]
		//	rb = gameObject.AddComponent<Rigidbody2D> ();
		//}
		if (speed <= 0) {

			speed = 1.6f;

			Debug.LogWarning ("Speed not set on " + name + ". Defaulting to " + speed);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isFacingLeft) 
			rb.velocity = new Vector2 (-speed, rb.velocity.y);
		else
			rb.velocity = new Vector2 (speed, rb.velocity.y);
	}

	void OnTriggerEnter2D(Collider2D c){
	
		if (c.gameObject.tag == "Enemy_barrier" ) {
			flip ();
			Debug.Log ("Turtle OnTriggerEnter2D" );
		}
	}
	void OnCollisionEnter2D(Collision2D c){

		if (c.gameObject.tag == "Enemy") {
			flip ();
			Debug.Log ("Turtle OnCollisionEnter2D " );
		}
		if (c.gameObject.tag == "Mario_Projectile") {
			Destroy (gameObject);
			SoundManager.instance.playSingleSound (killSFX);
			Debug.Log ("Turtle destroy");
		}
	}

	void flip(){

		isFacingLeft = !isFacingLeft;

		Vector3 scaleFactor = transform.localScale;

		scaleFactor.x = -scaleFactor.x;

		transform.localScale = scaleFactor;

		//projectileSpeed = -projectileSpeed;

	}
}
