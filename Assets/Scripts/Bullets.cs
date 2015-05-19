using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {

	private float speed = 10.0f;

	public int damage;
	public Vector3 direction;
	public Transform target;
	
	// Use this for initialization
	void Start () {
		direction = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// Move the bullet towards the direction of enemy
		transform.position += direction * Time.deltaTime * speed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Check if collides with enemy
		if (other.gameObject.tag == "Enemy" || other.GetComponent<Enemy>()) {
			// Deal damage to enemy (Not working yet)
			other.GetComponent<Enemy>().health -= damage;
			// Destroy bullet once hit enemy
			Destroy (gameObject);
		} 
	}
}
