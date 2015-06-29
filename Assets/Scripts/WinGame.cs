using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinGame : MonoBehaviour {
	public static int kills;
	//public GameObject winGame;

	// Use this for initialization
	void Start () {
		kills = 0;
		//winGame = GameObject.Find ("WinGame");
	}
	
	// Update is called once per frame
	void Update () {
		print (kills);
		if (kills >= 10) {
			//winGame.GetComponent<Text> ().enabled = true;
		} 
		else {
			//winGame.GetComponent<Text> ().enabled = false;
		}
	}
}
