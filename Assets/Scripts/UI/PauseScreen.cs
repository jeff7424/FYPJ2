using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScreen : MonoBehaviour {
	private GameObject game;
	GameObject screenFade;

	// Use this for initialization
	void Awake () {
		game = GameObject.Find ("Game");
		screenFade = GameObject.Find ("Screenfade");
	}
	
	public void EnablePanel() {
		// Enable info panel
		GetComponent<Image> ().enabled = true;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(true);
		}
		screenFade.GetComponent<Image>().enabled = true;
	}
	
	public void DisablePanel() {
		// Disable info panel
		GetComponent<Image> ().enabled = false;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
		screenFade.GetComponent<Image>().enabled = false;
	}
}
