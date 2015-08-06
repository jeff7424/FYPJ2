using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinningScreen : MonoBehaviour {

	private GameObject game;
	private bool activate = false;
	private float gameTime;
	private int defenseUsed;
	private int defenseDeleted;

	public Text timeElapsedText;
	public Text defenseUsedText;
	public Text defenseDeletedText;

	// Use this for initialization
	void Start () {
		game = GameObject.Find("Game");
		gameTime = 0;
		defenseUsed = 0;
		defenseDeleted = 0;
		timeElapsedText.text = "Time Elapsed: " + gameTime;
		defenseUsedText.text = "Defenses Used: " + defenseUsed;
		defenseDeletedText.text = "Defense Deleted: " + defenseDeleted;
	}
	
	// Update is called once per frame
	void Update () {
		if (activate) {
//			transform.Find ("Tower name").GetComponent<Text> ().text = defense.name;
//			transform.Find ("Damage").GetComponent<Text> ().text = "Damage: " + defense.damage;
//			transform.Find ("Firing rate").GetComponent<Text> ().text = "Fire rate: " + defense.GetFireRate ();
//			transform.Find ("Range").GetComponent<Text> ().text = "Range: " + defense.GetRange ();
//			transform.Find ("Special").GetComponent<Text>().text = "Level: " + defense.GetLevel();
			timeElapsedText.text = "Time Elapsed: " + gameTime.ToString("F2");
			defenseUsedText.text = "Defenses Used: " + defenseUsed;
			defenseDeletedText.text = "Defense Deleted: " + defenseDeleted;

		}
	}

	public void EnablePanel() {
		activate = true;
		GetComponent<Image> ().enabled = true;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(true);
		}

	}

	public void DisablePanel() {
		activate = false;
		GetComponent<Image> ().enabled = false;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void NextLevel() {
		PlayerPrefs.SetInt ("level", PlayerPrefs.GetInt ("level") + 1);
		Application.LoadLevel ("Game");
	}

	public void RestartLevel() {
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void BeckToLevelSelection() {
		Application.LoadLevel ("Level Select");
	}

	public void SetGameTime(float newTime) {
		gameTime = newTime;
	}

	public void SetDefenseUsed(int newValue) {
		defenseUsed = newValue;
	}

	public void SetDefenseDeleted(int newValue) {
		defenseDeleted = newValue;
	}
}
