using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScreen : MonoBehaviour {
	private bool enabledPauseMenu = false;
	private GameObject game;

	// Use this for initialization
	void Awake () {
		game = GameObject.Find ("Game");
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void EnablePanel() {
		// Enable info panel
		enabledPauseMenu = true;
		GetComponent<Image> ().enabled = true;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(true);
		}
	}
	
	public void DisablePanel() {
		// Disable info panel
		enabledPauseMenu = false;
		GetComponent<Image> ().enabled = false;
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}
}
