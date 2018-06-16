using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	public int health;
	public RectTransform healthBar;
	float healthScale;

	// Use this for initialization
	void Start () {
		if (health <= 0) {
			health = 5;
			Debug.LogWarning ("Health not set on " + name + "defaulting to 5");
		}
		if (!healthBar) {

			Debug.LogWarning ("healthBar not set on " + name);
		}
		healthScale = healthBar.sizeDelta.x / health;
	}
	
	void OnCollisionEnter2D(Collision2D c){
	
		if (c.gameObject.tag == "Mario_Projectile"){
			health--;
			Destroy (c.gameObject);
			healthBar.sizeDelta = new Vector2 (health * healthScale, healthBar.sizeDelta.y);
		}
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
