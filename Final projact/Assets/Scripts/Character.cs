using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;


public class Character : MonoBehaviour {

	AudioClip BGMusic;

		//Character cc = c.GetComponent<Character> ();

	public AudioClip jumpSFX;
	public AudioClip fireSFX;
	public AudioClip dieSFX; 
	 

	Rigidbody2D rb;

	public bool isDieing;

	public float speed;

	public float jumpForce;
	public bool isGrounded;
	public LayerMask isGroundLayer;
	public Transform groundCheck;
	public float groundCheckRadius;
	//public Vector2 yVelocity = rb.velocity.y; 

	Animator anim;

	public Transform projectileSpawnPoint;
	public Projectile projectilePrefab;
	public float projectileSpeed;
	public bool isFacingLeft;

	static public int _lives;
	public int livesForCheacks;
	// Use this for initialization
	void Start () {
		BGMusic = FindObjectOfType<Level> ().BGMusic;
		isDieing = false;
		rb = GetComponent<Rigidbody2D> ();
		if (!rb) {
			
			Debug.LogWarning ("Rigidbody2D not found on" + name);
		}

		if (speed <= 0) {

			speed = 5.0f;

			Debug.LogWarning ("Speed not set on " + name + ". Defaulting to " + speed);
		}

		if (jumpForce <= 0) {

			jumpForce =10.5f;

			Debug.LogWarning ("Jump force not set on " + name + ". Defaulting to " + jumpForce);
		}

		if (!groundCheck) {

			Debug.LogWarning ("GroundCheck not found on" + name);
		}

		if (groundCheckRadius <= 0) {

			groundCheckRadius = 0.2f;

			Debug.LogWarning ("groundCheckRadius not set on " + name + ". Defaulting to " + groundCheckRadius);
		}

		anim = GetComponent<Animator> ();
		if (!anim) {

			Debug.LogWarning ("Animator not found on" + name);
		}

		if (!projectileSpawnPoint) {

			Debug.LogWarning ("projectileSpawnPoint not found on" + name);
		}

		if (!projectilePrefab) {

			Debug.LogWarning ("projectilePrefab not found on" + name);
		}

		if (projectileSpeed <= 0) {
			projectileSpeed = 20.0f;

			Debug.LogWarning ("projectileSpeed not found on" + name + " setting to: "+projectileSpeed);
		}
		if (lives == 0) {
			Debug.Log ("lives changed to 3");
			lives = 3 ;
		}

	}






	// Update is called once per frame
	void Update () {
		livesForCheacks = _lives;
		//Checkes when Mario's velocity is less then 0 (goes beck down after bouncing) whait 1 sec and restart level
		if (isDieing) {
			if (rb.velocity.y <= 0.0f ) {
				Debug.LogWarning ("restart level");

				rb.isKinematic = true;
				rb.simulated = false;
				StartCoroutine ("restartLevel");

				isDieing = false;

			}

		}

		float moveValue = Input.GetAxisRaw("Horizontal");

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, isGroundLayer);

		if (Input.GetButtonDown ("Jump") && isGrounded) {
		
			Debug.Log ("Jump");
			SoundManager.instance.playSingleSound(jumpSFX);

			rb.AddForce (Vector2.up * jumpForce , ForceMode2D.Impulse);
		}

		if (Input.GetButton ("Fire1")) {
			anim.SetBool ("isCtrlBool", true);

		} else {
			anim.SetBool ("isCtrlBool", false);
		}

	


		if (Input.GetButtonDown ("Fire2") && anim.GetBool ("isFire")) {
			anim.SetTrigger ("isCtrl");
			fire();
			SoundManager.instance.playSingleSound(fireSFX);

		}
		//if (Input.GetButton ("Fire2")) {
			//anim.SetTrigger ("isCtrl");
			//fire();

		//}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			anim.SetTrigger ("isUp");

			//Debug.Log ("isUp");
		}
		//else
			//anim.ResetTrigger ("isUp");

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			anim.SetTrigger ("isDown");
			//Debug.Log ("isUp");
		}
		else
			anim.ResetTrigger ("isDown");

		if (Input.GetKeyDown(KeyCode.UpArrow) && anim.GetBool("isBig") == false) {
			
			anim.SetBool ("isBig", true);
			//Debug.Log ("Pew Pew");
		}

		else if (Input.GetKeyDown(KeyCode.DownArrow) && anim.GetBool("isBig") == true && anim.GetBool("isFire") == false) {
			
			anim.SetBool ("isBig", false);
		}

		else if (Input.GetKeyDown(KeyCode.UpArrow) && anim.GetBool("isBig") == true) {
			
			anim.SetBool ("isFire", true);
		}

		else if (Input.GetKeyDown(KeyCode.DownArrow) && anim.GetBool("isFire") == true) {
			
			anim.SetBool ("isFire", false);
		}
	

		rb.velocity = new Vector2 (moveValue * speed, rb.velocity.y);

		anim.SetFloat ("speed", Mathf.Abs (moveValue));
		anim.SetBool ("isGrounded", isGrounded);


		if ((moveValue > 0 && isFacingLeft) || (moveValue < 0 && !isFacingLeft)) {
		
			flip ();
		}

	}

	void fire(){
	
		Projectile temp = Instantiate (projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

		//temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileSpeed, 0);
		temp.GetComponent<Rigidbody2D> ().velocity = projectileSpawnPoint.right * projectileSpeed;

		//temp.speed = projectileSpeed;

	}

	void OnTriggerEnter2D(Collider2D c){
		Debug.Log (c.gameObject.tag);
		//Checks if Mario is touching a mushroom if so sets the bool and triger for animation
		if(!isDieing){
			if (c.tag == "Collectable") {
				if (c.name == "mushroom(Clone)") {
					Debug.LogWarning ("mushroom. grow up");
					anim.SetTrigger ("isUp");
					anim.SetBool ("isBig", true);
				}
				//Checks if Mario is touching a fire_flower if so sets the bool and triger for animation
				if (c.name == "fire_flower(Clone)") {
					Debug.LogWarning ("flower. fire up");
					anim.SetTrigger ("isUp");
					anim.SetBool ("isFire", true);
				}
				Destroy (c.gameObject);
			}
			//Checks if Mario is touching a Pit if so maks him bounce and change bool for level restart
			if (c.tag == "Pits") {
				//Debug.LogWarning ("restart level");
				Debug.Log ("Pits");
				anim.SetTrigger ("isDieing");
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.AddForce (Vector2.up * 10.0f, ForceMode2D.Impulse);
				//System.Threading.Thread.Sleep(2000);
				//yield return new WaitForSeconds(1.0f);
				isDieing = true;
				lives--;
				c.isTrigger = false;
			}
			if (c.tag == "Enemy" && !anim.GetBool("isBig") && !anim.GetBool("isFire")) {
				Debug.Log ("Enemy");
				anim.SetTrigger ("isDieing");
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.AddForce (Vector2.up * 10.0f, ForceMode2D.Impulse);
				isDieing = true;
				lives--;
			}
			if (c.tag == "Hammer_Projectile" && !anim.GetBool("isBig") && !anim.GetBool("isFire")) {
				Debug.Log ("Hammer_Projectile ");
				anim.SetTrigger ("isDieing");
				rb.velocity = new Vector2 (rb.velocity.x, 0);
				rb.AddForce (Vector2.up * 10.0f, ForceMode2D.Impulse);
				isDieing = true;
				lives--;
			}
		}

	
	}

	void flip(){
	
		isFacingLeft = !isFacingLeft;

		Vector3 scaleFactor = transform.localScale;

		scaleFactor.x = -scaleFactor.x;

		transform.localScale = scaleFactor;
				
		projectileSpeed = -projectileSpeed;
	
	}

	public int lives {

		//get;
		//set;

		get { return _lives; }
		set { _lives = value;
			Debug.Log ("Lives changed to: " + _lives);
		}
	}

	IEnumerator restartLevel(){

		SoundManager.instance.playSingleMusic(BGMusic,0.2f,true,true);
		SoundManager.instance.playSingleSound(dieSFX);
		yield return new WaitForSeconds (2.5f);
		//rb.gravityScale = 1;
		rb.isKinematic = false;
		rb.simulated = true;
		yield return new WaitForSeconds (1.0f);
		if(lives <= 0)
			GameManager.instance.GameOver();
		else
			SceneManager.LoadScene ("LEVEL1");
	}

}
