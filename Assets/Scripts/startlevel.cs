using UnityEngine;
using System.Collections;

public class startlevel : MonoBehaviour {

	public Texture start;
	// Use this for initialization
	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2 - 150, Screen.height/2 + 50, 300, 100), start)){
			Application.LoadLevel("GameLevel");
		}
		
	}
}
