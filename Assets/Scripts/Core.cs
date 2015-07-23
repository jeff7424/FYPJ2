using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Core : MonoBehaviour {

	int health;
	private GameObject game;
	public Text healthValue;
	public ParticleSystem explosionEffect;

	// Use this for initialization
	void Start () {
		health = 10;
		healthValue.text = "Health: " + health;
		game = GameObject.Find ("Game");
	}
	
	// Update is called once per frame
	void Update () {
		healthValue.text = "Health: " + health;
		if (health <= 0) {
			ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (explosion.gameObject, explosion.startLifetime);
			Destroy (gameObject);
			Time.timeScale = 0.5f;
			//GameObject lose = GameObject.Find ("GameOver");
			//lose.GetComponent<Text>().enabled = true;
			if (!game.GetComponent<Game>().losegame) {
				game.GetComponent<Game>().losegame = true;
				game.GetComponent<Game>().EnableLosingScreen();
			}
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
			game.GetComponent<Game>().enemyLeft --;
			Destroy (other.gameObject);
		} 
	}
}
