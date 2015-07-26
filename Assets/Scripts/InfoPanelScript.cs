using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {

	public Defense defense;
	public Tile tile;
	private bool enabledDisplay = false;
	private GameObject game;

	private GameObject towerName;
	private GameObject damage;
	private GameObject firerate;
	private GameObject range;
	private GameObject special;

	// Use this for initialization
	void Awake () {
		game = GameObject.Find ("Game");
		if (Application.loadedLevelName == "Game") {
			towerName = GameObject.Find ("Tower name");
			damage = GameObject.Find ("Damage");
			firerate = GameObject.Find ("Firing rate");
			range = GameObject.Find ("Range");
			special = GameObject.Find ("Special");
		} else if (Application.loadedLevelName == "Multiplayer") {
			if (gameObject.tag == "Player 1") {
				towerName = GameObject.Find ("Tower name 1");
				damage = GameObject.Find ("Damage 1");
				firerate = GameObject.Find ("Firing rate 1");
				range = GameObject.Find ("Range 1");
				special = GameObject.Find ("Special 1");
			} else if (gameObject.tag == "Player 2") {
				towerName = GameObject.Find ("Tower name 2");
				damage = GameObject.Find ("Damage 2");
				firerate = GameObject.Find ("Firing rate 2");
				range = GameObject.Find ("Range 2");
				special = GameObject.Find ("Special 2");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enabledDisplay) {
			towerName.GetComponent<Text> ().text = defense.name;
			damage.GetComponent<Text> ().text = "Damage: " + defense.damage;
			firerate.GetComponent<Text> ().text = "Fire rate: " + defense.GetFireRate ();
			range.GetComponent<Text> ().text = "Range: " + defense.GetRange ();
			special.GetComponent<Text>().text = "Level: " + defense.GetLevel();
		}
	}

	public void EnablePanel() {
		// Enable info panel
		enabledDisplay = true;
		GetComponent<Image> ().enabled = true;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(true);
		}
	}

	public void DisablePanel() {
		// Disable info panel
		enabledDisplay = false;
		GetComponent<Image> ().enabled = false;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
		if (tile != null) {
			tile.GetComponent<Tile>().TileDeselected();
		}
	}

	public void DestroyDefense() {
		// If defense exist, destroy defense when function is called
		if (defense != null) {
			tile.GetComponent<Tile> ().DestroyDefense ();
			enabledDisplay = false;
			DisablePanel ();
			defense = null;
		}
	}

	public void RankUpDefense() {
		// If defense exist, rank up defense when function is called
		if (defense != null) {
			tile.GetComponent<Tile>().RankUpDefense();
		}
	}
}
