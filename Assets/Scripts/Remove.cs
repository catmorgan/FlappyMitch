using UnityEngine;
using System.Collections;

public class Remove : MonoBehaviour {
	
	private double timer = 0.0;	
	void Update () {
		if (!this.renderer.isVisible) {
			timer += Time.deltaTime;
			if (timer > 3) {
			Destroy (this.gameObject);
				timer = 0.0;
			}
		}
	}
}
