using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Core : MonoBehaviour {

	int health;
	private GameObject game;
	private GameObject player;
	public Text healthValue;
	public ParticleSystem explosionEffect;

	public int startHealth;
	public Slider healthSlider;
	//public Image damage;
	//public AudioClip explodeClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color(1.0f, 0.0f, 0.0f, 0.2f);


	// Use this for initialization
	void Start () {
		startHealth = 10;
		healthValue.text = "Health: " + health;
		game = GameObject.Find ("Game");
		if (Application.loadedLevelName == "Game")
			player = GameObject.Find ("Player");
		else if (Application.loadedLevelName == "Multiplayer") {
			player = GameObject.Find (gameObject.tag);
		}
		health = startHealth;
		healthSlider.value = health / startHealth;
		Debug.Log (healthSlider.value);
	}
	
	// Update is called once per frame
	void Update () {
		healthValue.text = "Health: " + health;
		if (health <= 0) {
			ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (explosion.gameObject, explosion.startLifetime);
			//Destroy (gameObject);
			GetComponent<SpriteRenderer>().enabled = false;
			Time.timeScale = 0.5f;

			if (!player.GetComponent<Player1>().losegame) {
				player.GetComponent<Player1>().losegame = true;
				player.GetComponent<Player1>().EnableLosingScreen();
				game.GetComponent<Game>().SetEndGame(true);

				GameObject player2 = null;
				if (gameObject.tag == "Player 1")
					player2 = GameObject.Find ("Player 2");
				else if (gameObject.tag == "Player 2")
					player2 = GameObject.Find ("Player 1");

				player2.GetComponent<Player1>().wingame = true;
				player2.GetComponent<Player1>().EnableWinningScreen();

			}
		}
	}

	public int CurrentHealth() {
		return health;
	}

	public void DecreaseHealth() {
		if (CurrentHealth () > 0)
			health -= 1;
		healthSlider.value = (float)((float)health / (float)startHealth);
		Debug.Log (healthSlider.value);
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Check if collides with enemy
		if (other.gameObject.tag == "Enemy" || other.GetComponent<Enemy>()) {
			DecreaseHealth ();
			player.GetComponent<Player1>().enemyLeft --;
			Destroy (other.gameObject);
		} 
	}
}
