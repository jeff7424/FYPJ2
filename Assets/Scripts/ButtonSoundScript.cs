using UnityEngine;
using System.Collections;

public class ButtonSoundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("volume");
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("volume");
	}
}
