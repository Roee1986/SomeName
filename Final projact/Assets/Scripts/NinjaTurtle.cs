using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class NinjaTurtle : MonoBehaviour {

	public AudioClip killSFX;

	Animator anim;

	public Transform projectileSpawnPoint;
	public GameObject player;
	public ProjectileHammer projectilePrefab;
	public float projectileSpeed;
	public bool isFacingLeft;
	public float projectileFireRate;
	float timeSinceLastFire;
	float distanceToPlayer;
	public float MinDistanceToFire;


	Rigidbody2D rb;
	public float speed;
	public int health;



	// Use this for initialization
	void Start () {
		//anim.SetBool ("inProximity", false);
		isFacingLeft = true;
		rb = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");

		if (!projectileSpawnPoint) {

			Debug.LogWarning ("projectileSpawnPoint not found on" + name);
		}

		if (!projectilePrefab) {

			Debug.LogWarning ("projectilePrefab not found on" + name);
		}

		if (projectileSpeed <= 0) {
			projectileSpeed = -20.0f;
		}
		if (projectileFireRate <= 0) {
			projectileFireRate = 2.0f;
		}
		if (health <= 0) {
			health = 4 ;
			Debug.LogWarning ("health not set on " + name + ". health = 4");
		}
		if (MinDistanceToFire <= 0) {
			MinDistanceToFire = 8.0f ;
			Debug.LogWarning ("MinDistanceToFire not set on " + name + ". MinDistanceToFire = 8");
		}


	}

	// Update is called once per frame
	void FixedUpdate () {
		
		if (player) {

			distanceToPlayer = Vector2.Distance (player.transform.position, transform.position);
			//Debug.Log (distanceToPlayer);
		}

		projectileSpeed = distanceToPlayer;

		if (Time.time > timeSinceLastFire + projectileFireRate && distanceToPlayer <= MinDistanceToFire) {
			
			fire ();
			//anim.SetTrigger("Attack");
			timeSinceLastFire = Time.time;
			Debug.Log ("MinDistanceToFire: "+ MinDistanceToFire + ". distanceToPlayer: " + distanceToPlayer);
		}
		//if(distanceToPlayer < MinDistanceToFire)
			//anim.SetBool ("inProximity", true);
	//	else
			//anim.SetBool ("inProximity", false);
		
		if (((player.transform.position.x < transform.position.x && !isFacingLeft) || (player.transform.position.x > transform.position.x && isFacingLeft)) && distanceToPlayer <= MinDistanceToFire) {

			flip ();
		}
		if (distanceToPlayer >= MinDistanceToFire) {
			//anim.SetBool ("inProximity", false);
			if (isFacingLeft) {
				rb.velocity = new Vector2 (-speed, rb.velocity.y);
				Debug.LogError ("NT walk left");
			} else {
				rb.velocity = new Vector2 (speed, rb.velocity.y);
				Debug.LogError ("NT walk Right");
			}
		} else {
			//anim.SetBool ("inProximity", true);
		}

			
	}

	void flip(){

		isFacingLeft = !isFacingLeft;

		Vector3 scaleFactor = transform.localScale;

		scaleFactor.x = -scaleFactor.x;

		transform.localScale = scaleFactor;

		projectileSpeed = -projectileSpeed;

	}
	void fire(){

		ProjectileHammer temp = Instantiate (projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
		Debug.LogWarning ("Instantiate");
		//temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileSpeed, 0);
		//temp.GetComponent<Rigidbody2D> ().velocity = projectileSpawnPoint.right * projectileSpeed;

		if (isFacingLeft) {
			temp.speed = -projectileSpeed;
			Debug.LogWarning ("left");
		} else {
			temp.speed = projectileSpeed;
			Debug.LogWarning ("right");
		}
		Debug.LogWarning ("projectileSpeed " + projectileSpeed );

		//temp.tag = "Hammer_Projectile"; Change the tag

	}
	void OnCollisionEnter2D(Collision2D c){
	
		if (c.gameObject.tag == "Mario_Projectile") {
		
			health--;
			//add projectile destroy
			//health -= c.gameObject.GetComponent<Projectile>().GetDamage();

			if (health <= 0) {
				Destroy (gameObject);//(gameObject, time of death animation)
				SoundManager.instance.playSingleSound(killSFX);
				Debug.Log ("ninja Turtle destroy");
			}
		}
		if (c.gameObject.tag == "Enemy") {
			flip ();
			Debug.Log ("ninja Turtle OnCollisionEnter2D " );
		}
	}

	void OnTriggerEnter2D(Collider2D c){

		if (c.gameObject.tag == "Enemy_barrier" ) {
			flip ();
			Debug.Log ("ninja Turtle OnTriggerEnter2D" );
		}
	}

}
