using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {

	public Defense defense;
	public Tile tile;
	private bool enabledDisplay = false;
	private GameObject game;

	public GameObject towerName;
	public GameObject damage;
	public GameObject firerate;
	public GameObject range;
	public GameObject special;
	public GameObject upgradeCost;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
		if (Application.loadedLevelName == "Game") {
			towerName = GameObject.Find ("Tower name");
			damage = GameObject.Find ("Damage");
			firerate = GameObject.Find ("Firing rate");
			range = GameObject.Find ("Range");
			special = GameObject.Find ("Special");
			upgradeCost = GameObject.Find ("UpgradeCost");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enabledDisplay) {
			towerName.GetComponent<Text> ().text = defense.name;
			damage.GetComponent<Text> ().text = defense.damage.ToString();
			firerate.GetComponent<Text> ().text = defense.GetFireRate ().ToString();
			range.GetComponent<Text> ().text = defense.GetRange ().ToString();
			special.GetComponent<Text>().text = defense.GetLevel().ToString();
			upgradeCost.GetComponent<Text>().text = "Cost: " + defense.GetCost().ToString();
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

	public void SetSelection(Tile target, Defense targetchild) {
		EnablePanel ();
		tile = target;
		defense = targetchild;
	}
}
