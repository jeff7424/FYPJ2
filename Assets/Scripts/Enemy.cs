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
			Destroy (gameObject);
		}
	}

	public float getSpeed(){
		return speed;
	}
}
