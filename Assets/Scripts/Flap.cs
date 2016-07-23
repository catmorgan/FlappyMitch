using UnityEngine;
using System.Collections;

public class Flap : MonoBehaviour {
	
	public float flapSpeed; 
	public float jumpHeight = 12;
	private PlayerPhysics physics;
	private Vector2 movement;
	public float gravity = 20;
	public AudioSource mitch;
	private bool play = false;
	
	void Start () {
		physics = GetComponent<PlayerPhysics>();
	}
	
	void Update () {
		
		if (Input.GetButtonDown("Jump")) {
				movement.y = jumpHeight;
			play = true;
			}
		
		if (play) {
			mitch.Play();
		}
		
		movement.x = flapSpeed;
		movement.y -= gravity * Time.deltaTime;
		physics.move(movement * Time.deltaTime);
		play = false;
	}
}
