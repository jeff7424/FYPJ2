using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {

	public Defense defense;
	public Tile tile;
	private bool enabledDisplay = false;
	private GameObject game;

	// Use this for initialization
	void Awake () {
		game = GameObject.Find ("Game");
	}
	
	// Update is called once per frame
	void Update () {
		if (enabledDisplay) {
			transform.Find ("Tower name").GetComponent<Text> ().text = defense.name;
			transform.Find ("Damage").GetComponent<Text> ().text = "Damage: " + defense.damage;
			transform.Find ("Firing rate").GetComponent<Text> ().text = "Fire rate: " + defense.GetFireRate ();
			transform.Find ("Range").GetComponent<Text> ().text = "Range: " + defense.GetRange ();
			transform.Find ("Special").GetComponent<Text>().text = "Level: " + defense.GetLevel();
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
