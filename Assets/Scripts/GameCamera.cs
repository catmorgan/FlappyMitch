using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	//camera movement speed
	public float cameraWalkSpeed = 10;
	public float cameraRunSpeed = 14;
	
	private Transform target;
	
	//camera follows the target object
	public void followTarget(Transform t) {
		target = t;
	}
	
	void LateUpdate() {
		//set camera positions to follow the players positions
		if (target) {
			float cameraSpeed = (Input.GetButton("Run"))? cameraRunSpeed:cameraWalkSpeed;
			float x = moveTowards(transform.position.x, target.position.x, cameraSpeed);
			//float y = moveTowards(transform.position.y, target.position.y + 5, cameraSpeed);
			float y = moveTowards(transform.position.y, transform.position.y, cameraSpeed);
			transform.position = new Vector3(x, y, transform.position.z);
		}
	}
	
	//target to increment towards
	private float moveTowards(float current, float target, float accel) {
		//we've reached out target
		if (current == target) { 
			return current;
		} else {
			//get the direction of current to target
			float dir = Mathf.Sign(target-current);
			//increment the current to be towards the target
			current += dir * accel * Time.deltaTime;
			//if we're towards the target, return current, else we are away
			//from target so return target
			return (dir == Mathf.Sign(target-current))? current:target;
		}
	}
}
