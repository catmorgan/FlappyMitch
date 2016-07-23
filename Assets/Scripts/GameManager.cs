using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	public GameObject pillar;
	private GameCamera cam;
	private Vector3 pos;
	
	private PlayerStats stats;
	public int highestScore;
	private AudioSource audio;
	
	// set up camera and spawn player
	void Start () {
		cam = GetComponent<GameCamera>();
		pos = cam.transform.position;
		Spawn ();
		highestScore = 0;
		audio = GetComponent<AudioSource>();
	}
	
	void Update() {
		if (GameObject.FindGameObjectsWithTag("Player").Length == 0) {
			audio.Play();
			cam.transform.position = pos;
			Spawn ();
		}
		
		stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
		
		if (stats.score > highestScore) {
			highestScore = stats.score;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	
	void OnGUI() {
		
	GUI.skin.label.alignment = TextAnchor.UpperCenter;
	GUI.skin.label.fontSize = 30;
	GUI.skin.label.fontStyle = FontStyle.Bold;
	GUI.skin.label.wordWrap = true;
	GUI.skin.label.normal.textColor = Color.white;
		
	GUI.Label(new Rect (Screen.width - 310, 20, 300, 50),"highest score: " + highestScore);
	}
	
	//spawn the player and have camera follow it
	private void Spawn() {
		cam.followTarget((Instantiate(player, new Vector3(0,5,0), Quaternion.identity) as GameObject).transform);
		
		GameObject[] pillars = GameObject.FindGameObjectsWithTag("Score");
		foreach (GameObject p in pillars) {
			Destroy (p.transform.parent.gameObject);
		}
				Instantiate(pillar, new Vector3(2, 5, -2.908936f), Quaternion.identity);
	}
}
