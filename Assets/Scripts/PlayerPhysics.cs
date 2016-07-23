using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask collisionMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool stopped;
	public GameObject pillar;
	public GameObject photo;
	private AudioSource[] ping;
	
	//box collider
	new private BoxCollider collider = new BoxCollider();
	private Vector3 size;
	private Vector3 center;
	private Vector3 originalSize;
	private Vector3 originalCenter;
	private float colliderScale;
	//distance between player and ground
	private float space = .005f;
	private PlayerStats playerStats;
	
	private float numOfCollisionRaysX = 3;
	private float numOfCollisionRaysY = 10;
	
	Ray ray;
	RaycastHit hit;
	private float timer = 0;
	
	public void Start() {
		//get player's collider
		collider = GetComponent<BoxCollider>();
		playerStats = GetComponent<PlayerStats>();
		colliderScale = transform.localScale.x;
		originalSize = collider.size;
		originalCenter = collider.center;
		SetCollider(originalSize, originalCenter);
		ping = GetComponents<AudioSource>();
	}

	public void move(Vector2 moveAmount) {
		//x and y to be changed
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 position = transform.position;
		
		//above and below collisions
		grounded = false;
		for (int i = 0; i < numOfCollisionRaysX; i++) {
			float dir = Mathf.Sign(deltaY);
			//left, center, and right position for raycast
			float x = (position.x + center.x - size.x /2) + size.x/(numOfCollisionRaysX - 1) * i;
			//bottom of collider
			float y = position.y + center.y + size.y /2 * dir;
			ray = new Ray(new Vector2(x,y), new Vector2(0, dir));
			Debug.DrawRay(ray.origin, ray.direction);
			//if the ray hits a collide-able object above or below
			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + space, collisionMask)){
				//distance between player and hit object
				float dist = Vector3.Distance(ray.origin, hit.point);
				
				//stops player at object
				if (dist > space) {
					deltaY = dist  * dir - space * dir;
				} else {
					deltaY = 0;
				}

				grounded = true;
				break;
			}
		}
		
		stopped = false;
		//left and right collisions
		for (int i = 0; i < numOfCollisionRaysY; i++) {
			float dir = Mathf.Sign(deltaX);
			//left, center, and right position
			float x = position.x + center.x + size.x /2 * dir;
			//side of collider
			float y = position.y + center.y - size.y /2 + size.y/(numOfCollisionRaysY-1) * i;
			ray = new Ray(new Vector2(x,y), new Vector2(dir,0));
			Debug.DrawRay(ray.origin, ray.direction);
			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + space, collisionMask)){
				//distance between player and ground
				float dist = Vector3.Distance(ray.origin, hit.point);
				
				//stops player at ground
				if (dist > space) {
					deltaX = dist  * dir - space * dir;
				} else {
					deltaX = 0;
				}
				stopped = true;
				break;
			}
		}
		
		//collision of player's falling direction
		if (!grounded && !stopped) {
			Vector3 playersDir = new Vector3(deltaX, deltaY);
			Vector3 origin = new Vector3(position.x + center.x + size.x /2 * Mathf.Sign(deltaX), 
				position.y + center.y + size.y /2 * Mathf.Sign(deltaY));
			ray = new Ray(origin, playersDir.normalized);
		
			if(Physics.Raycast(ray, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask)) {
				grounded = true;
				deltaY = 0;
			}
			
		}
		
		//return the final translation after collision detection
		Vector2 finalMovement = new Vector2(deltaX, deltaY);
		transform.Translate(finalMovement);
		
	}
	
	public void SetCollider (Vector3 s, Vector3 c) {
		collider.size = s;
		collider.center = c;
		
		size = s * colliderScale;
		center = c * colliderScale;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Enemy") || 
			other.gameObject.CompareTag("Obstacle")) {
			playerStats.dead = true;
		}
		
		if (other.gameObject.CompareTag("Score")) {
			
			if (!playerStats.dead) {
			Vector3 newPos = other.gameObject.transform.position;
			newPos.x = other.gameObject.transform.position.x + 5;
			newPos.y = Random.Range(0.5f,6.3f);
			newPos.z = -2.9f;
			Instantiate(pillar, newPos, Quaternion.identity);
			timer += Time.deltaTime;
				if (timer > 1) {
					Destroy (other.gameObject.transform.parent);
					timer = 0;
				}
				
			playerStats.score++;
			ping[1].Play();
			}
		}
		
	if (other.gameObject.CompareTag("Photo")) {
			Vector3 newPos = new Vector3 (other.gameObject.transform.position.x + 7,10,0);
			Instantiate (photo, newPos, Quaternion.identity);
			timer += Time.deltaTime;
			if (timer > 3) {
			Destroy (other.gameObject.transform.parent.gameObject);
				timer = 0;
			}
		}
	}

}
