using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerScript : MonoBehaviour {

	GameObject game;

	GameObject EnemyParent;
	GameObject[] towers;

	GameObject slowbutton;
	GameObject ragebutton;
	GameObject kamikazebutton;

	private float cooldown_slow;
	private float cooldown_rage;
	private float cooldown_kamikaze;

	private float cooldown_slow_original = 10.0f;
	private float cooldown_rage_original = 10.0f;
	private float cooldown_kamikaze_original = 10.0f;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
		if (Application.loadedLevelName == "Game") {
			slowbutton = GameObject.Find ("Slow Power");
			ragebutton = GameObject.Find ("Rage Power");
			kamikazebutton = GameObject.Find ("Kamikaze");
			EnemyParent = GameObject.Find ("EnemyParent");
		} else if (Application.loadedLevelName == "Multiplayer") {
			if (gameObject.tag == "Player 1") {
				slowbutton = GameObject.Find ("Slow Power 1");
				ragebutton = GameObject.Find ("Rage Power 1");
				kamikazebutton = GameObject.Find ("Kamikaze 1");
				EnemyParent = GameObject.Find ("EnemyParent 1");
			} else if (gameObject.tag == "Player 2") {
				slowbutton = GameObject.Find ("Slow Power 2");
				ragebutton = GameObject.Find ("Rage Power 2");
				kamikazebutton = GameObject.Find ("Kamikaze 2");
				EnemyParent = GameObject.Find ("EnemyParent 2");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!game.GetComponent<Game>().isPause) {
			towers = GameObject.FindGameObjectsWithTag("Defense");
			if (cooldown_slow > 0.0f) {
				cooldown_slow -= Time.deltaTime;
			} else {
				cooldown_slow = 0.0f;
				slowbutton.GetComponent<Button>().interactable = true;
			}

			if (cooldown_rage > 0.0f) {
				cooldown_rage -= Time.deltaTime;
			} else {
				cooldown_rage = 0.0f;
				ragebutton.GetComponent<Button>().interactable = true;
			}

			if (cooldown_kamikaze > 0.0f) {
				cooldown_kamikaze -= Time.deltaTime;
			} else {
				cooldown_kamikaze = 0.0f;
				kamikazebutton.GetComponent<Button>().interactable = true;
			}
		}
	}

	public void SlowEnemies() {
		if (cooldown_slow <= 0.0f && !game.GetComponent<Game>().isPause) {
			EnemyParent.GetComponent<EnemyParentScript>().SlowEnemies();
			cooldown_slow = cooldown_slow_original;
			slowbutton.GetComponent<Button>().interactable = false;
		}
	}

	public void Rage() {
		if (cooldown_rage <= 0.0f && !game.GetComponent<Game>().isPause) {
			foreach (GameObject tower in towers) {
				tower.GetComponent<Defense>().Rage (3.0f, 2.0f);
				cooldown_rage = cooldown_rage_original;
				ragebutton.GetComponent<Button>().interactable = false;
			}
		}
	}

	public void Kamikaze() {
		if (cooldown_kamikaze <= 0.0f && !game.GetComponent<Game>().isPause) {
			EnemyParent.GetComponent<EnemyParentScript>().Kamikaze();
			cooldown_kamikaze = cooldown_kamikaze_original;
			kamikazebutton.GetComponent<Button>().interactable = false;
		}
	}
}
