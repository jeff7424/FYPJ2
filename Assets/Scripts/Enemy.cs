using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int health;
	private float speed;

	// Use this for initialization
	void Start () {
		health = 10;
		speed = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		// If health less than zero destroy object
		if (health <= 0) {
			WinGame.kills++;
			Destroy (gameObject);
		}
	}

	public float getSpeed(){
		return speed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Check if collides with enemy
		if (other.gameObject.tag == "Core" || other.GetComponent<Core>()) {
			// Deal damage to enemy (Not working yet)
			// Destroy bullet once hit enemy
			//Destroy (other.gameObject);
		} 
	}
}
