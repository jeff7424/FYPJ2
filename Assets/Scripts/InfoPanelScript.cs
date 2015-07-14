using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelScript : MonoBehaviour {

	public Defense defense;
	public Tile tile;
	private bool enabledDisplay = false;
	private GameObject game;

	// Use this for initialization
	void Start () {
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
		enabledDisplay = true;
		GetComponent<Image> ().enabled = true;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(true);
		}
	}

	public void DisablePanel() {
		enabledDisplay = false;
		GetComponent<Image> ().enabled = false;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void DestroyDefense() {
//		if (defense != null) {
//			Destroy (defense.gameObject);
//			enabledDisplay = false;
//			DisablePanel ();
//			defense = null;
//		}
		if (defense != null) {
			tile.GetComponent<Tile> ().DestroyDefense ();
			enabledDisplay = false;
			DisablePanel ();
			defense = null;
		}
	}

	public void RankUpDefense() {
		if (defense != null) {
			tile.GetComponent<Tile>().RankUpDefense();
		}
	}
}
