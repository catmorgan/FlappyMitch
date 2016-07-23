using UnityEngine;
using System.Collections;

public class CreatePillar : MonoBehaviour {
	
	public GameObject pillar;
//
//	// Use this for initialization
//	void Start () {
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		Debug.Log(Screen.width);
//		if (this.transform.position.x < Screen.width - 20){
//			Vector3 newPos = randomizePos();
//			Instantiate(pillar, newPos, Quaternion.identity);
//		}
//	}
//	
	public void createPillar() {
		float posX = this.transform.position.x + 5;
		float posY = Random.Range(0f,6.3f);
		Vector3 newPos = new Vector3(posX, posY, 0);
		Instantiate(pillar, newPos, Quaternion.identity);
	}
}
