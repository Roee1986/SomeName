using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	static GameManager _instance = null;
	int _score;
	public Text scoreText;
	bool isPause;
	public Button button_Quit;
	public CanvasGroup canvas;
	//float button_Quit_Alpha;
	Color alpha;
	public GameObject playerPrefab;
	private GameObject player;
	// Use this for initialization
	void Start () {
		//button_Quit_Alpha = 0.0f;
		//alpha.a = canvas.GetComponent<Image>().color.a;
		isPause = false;

		if (instance)
			Destroy (gameObject);
		else {
			instance = this;
			DontDestroyOnLoad (this);
		}
		score = 0;
		player = GameObject.FindGameObjectWithTag ("Player");


	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("update");

		//button_Quit.GetComponent<Image>().color.a = button_Quit_Alpha ;
	//	if (Input.GetKeyDown (KeyCode.Escape)) {

		//	if(SceneManager.GetActiveScene().name == "Screen_Title")
			//	SceneManager.LoadScene ("LEVEL1");
			//else if(SceneManager.GetActiveScene().name == "LEVEL1")
				//SceneManager.LoadScene ("Screen_Title");
		//}
		if (SceneManager.GetActiveScene ().name == "LEVEL1") {
			Debug.Log("IN LEVEL1");
		if (Input.GetKeyDown (KeyCode.P) && !isPause) {
			Debug.Log("P");
			//if (SceneManager.GetActiveScene ().name == "LEVEL1") {
				//Debug.Log("IN LEVEL1");
				//if (isPause) {
					//System.Threading.Thread.Sleep (1000);
					//button_Quit_Alpha = 1.0f;
				Debug.Log("pause");
					//Time.timeScale = 1;
					//alpha.a = 1.0f;
				canvas.alpha = 1.0f;
					//Debug.Log(alpha.a);
					//if (Input.GetKeyDown (KeyCode.P)) {
				isPause = true;
					//	Debug.Log("not pause");
						//Time.timeScale = 0;

					//}
		} 
		else if(Input.GetKeyDown (KeyCode.P) && isPause)
		{
				//button_Quit_Alpha = 0.0f;
			Debug.Log("not pause");

				//alpha.a = 0.0f;
			canvas.alpha = 0.0f;
				//Time.timeScale = 1;
				//Debug.Log (alpha.a);
				//if (Input.GetKeyDown (KeyCode.P)) {
			isPause = false;
				//	Debug.Log("pause");
					//Time.timeScale = 0;
				//}
		}
			if(canvas.alpha == 1.0f)
				Time.timeScale = 0f;
			else
				Time.timeScale = 1.0f;
		}
		

		if (player && player.GetComponent<Character> ().lives <= 0) {
			SceneManager.LoadScene ("Game_Over");
		}
		if (canvas) {
			//Debug.Log ("canvas is here");
			//canvas.GetComponent<Image> ().color = alpha;

			Debug.Log("Change Alpha to: " + canvas.GetComponent<Image> ().color.a);
			
		}
		
	}

	public void spawnPlayer(int spawnLocation)
	//public void spawnPlayer(Transform spawnLocation)
	//public void spawnPlayer(Vector3 spawnLocation)
	//public void spawnPlayer(GameObject spawnLocation)
	{
		string spawnPointName = SceneManager.GetActiveScene ().name + "_" + spawnLocation;
		//LEVEL1_0

		Transform spawnPointTransform = GameObject.Find (spawnPointName).GetComponent<Transform> ();
		if (playerPrefab && spawnPointTransform)
			Instantiate (playerPrefab, spawnPointTransform.position, spawnPointTransform.rotation);
		else
			Debug.Log ("Missing playerPrefab or spawnPointTransform");
	}
	public static GameManager instance {

		get { return _instance; }
		set { _instance = value;
			Debug.Log ("instance changed to: " + _instance);
		}
	}

	public void StartGame(){
		SceneManager.LoadScene ("LEVEL1");
	}

	public void QuitGame(){
	
		Debug.Log ("Quitting...");
		Application.Quit();

	}

	public int score {

		get { return _score; }
		set { _score = value;
			Debug.Log ("score changed to: " + _score);
			if(scoreText)
				scoreText.text = "Score: " + score;
		}
	}
	public void GameOver(){

		if(SceneManager.GetActiveScene().name == "LEVEL1")
			SceneManager.LoadScene ("Game_Over");
	}
}
