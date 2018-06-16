using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ExtraLife : MonoBehaviour {

	Rigidbody2D rb;
	BoxCollider2D bc;

	public AudioClip collectableSFX;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();

		rb.gravityScale = 0;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;


		bc.isTrigger = true;
	}
	
	void OnTriggerEnter2D(Collider2D c){
	
		if (c.gameObject.tag == "Player") {
		
			Character cc = c.GetComponent<Character> ();
			if (cc) {
				cc.lives++;
				GameManager.instance.score += 10;
				Debug.Log ("Score: " + GameManager.instance.score);
				Debug.LogWarning ("Lives++");

				//AudioSource.PlayClipAtPoint (collectableSFX, Camera.main.transform.position);
				SoundManager.instance.playSingleSound(collectableSFX);
			}

			Destroy (gameObject);
		}
	}
}
