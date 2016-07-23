using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	public bool dead = false;
	
	public int score = 0;
	public GameObject pillar;
	public Texture space;
	bool show = true;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < -7) {
			dead = true;
		}
		if (dead) {
			Destroy (this.gameObject);
//			if (score > 1) {
//			Instantiate(pillar, new Vector3(2,Random.Range(0f, 6f), -2.9f), Quaternion.identity);
//			}
		}
		
		
	}
	
	void OnGUI() {
		
	if (show) {
		GUI.Box(new Rect(Screen.width/2 - 150, Screen.height/2 + 50, 300, 75), space);
		if (Input.GetButtonDown("Jump")) {
			this.GetComponent<Flap>().enabled = true;
				show = false;
			
		}	
	}
		
	GUI.skin.label.alignment = TextAnchor.UpperCenter;
	GUI.skin.label.fontSize = 30;
	GUI.skin.label.fontStyle = FontStyle.Bold;
	GUI.skin.label.wordWrap = true;
	GUI.skin.label.normal.textColor = Color.white;
		
	GUI.Label(new Rect (10, 20, 200, 50),"score: " + score);
		
	}
}
