using UnityEngine;
using System.Collections;

public class MainMenuBGM : MonoBehaviour {

	public static MainMenuBGM Instance;

	// Use this for initialization
	void Awake () {
		if(Instance)
		{
			DestroyImmediate(this.gameObject);
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
			Instance = this;
		}
	}

	void Update() {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat ("volume");
		if (Application.loadedLevelName == "Main Menu" || Application.loadedLevelName == "Level Select") {
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().Play ();
			}
		}
		else
			GetComponent<AudioSource>().Stop ();
	}
}
