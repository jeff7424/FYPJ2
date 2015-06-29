using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Core : MonoBehaviour {

	int health;
	public GameObject thePathfinderRoot;

	public GameObject pathNode;

	public Text healthValue;
	public ParticleSystem explosionEffect;

	// Use this for initialization
	void Start () {
		health = 10;
		healthValue.text = "Health: " + health;
	}
	
	// Update is called once per frame
	void Update () {
		healthValue.text = "Health: " + health;
		if (health <= 0) {
			ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (explosion.gameObject, explosion.startLifetime);
			Destroy (gameObject);
			Time.timeScale = 0.5f;
			GameObject lose = GameObject.Find ("GameOver");
			lose.GetComponent<Text>().enabled = true;
		}
	}

	public int CurrentHealth() {
		return health;
	}

	public void DecreaseHealth() {
		if (CurrentHealth () > 0)
			health -= 1;
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Check if collides with enemy
		if (other.gameObject.tag == "Enemy" || other.GetComponent<Enemy>()) {
			DecreaseHealth ();
			Destroy (other.gameObject);
		} 
	}
}
