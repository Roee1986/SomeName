using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	
	public float powerUpTimer;
	public float jumpStrength;

	Character cc;

	// Use this for initialization
	void Start () {
		if (powerUpTimer <= 0) {
			powerUpTimer = 20.0f;
			Debug.LogWarning ("timer not set on " + name + "defaulting to " + powerUpTimer);
		}

		if (jumpStrength <= 0) {
			jumpStrength = 20.0f;
			Debug.LogWarning ("jump force not set on " + name + "defaulting to " + jumpStrength);
		}

		if (!GetComponent<BoxCollider2D> ()) {
			gameObject.AddComponent<BoxCollider2D> ();

			GetComponent<BoxCollider2D> ().isTrigger = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D c){

		if (c.gameObject.tag == "Player") {

			cc = c.GetComponent<Character> ();
			if (cc) {
				
				cc.jumpForce += jumpStrength;
				StartCoroutine ("stopPowerUp", cc);
				Debug.LogWarning ("PowerUp");
			}

			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

	IEnumerator stopPowerUp(Character c){
	
		yield return new WaitForSeconds (powerUpTimer);

		cc.jumpForce -= jumpStrength;
	}


}
