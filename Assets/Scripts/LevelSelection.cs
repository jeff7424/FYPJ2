using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelSelection : MonoBehaviour {

	public int level;
	private bool tutorial;

	// Use this for initialization
	void Start () {
		level = 0;
		tutorial = false;
		PlayerPrefs.SetInt ("level", level);
		Load();
	}

	void Load() {
		if (File.Exists (Application.persistentDataPath + "/Tutorial")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/Tutorial", FileMode.Open);
			TutorialOver t = (TutorialOver)bf.Deserialize(file);
			file.Close ();
			tutorial = t.TutorialDone;
			Debug.Log ("Loaded successfully");
		}
	}

	public void StartLevel(int newValue) {
		level = newValue;
		PlayerPrefs.SetInt ("level", level);
		Application.LoadLevel ("Game");
	}

	public void StartMultiplayer() {
		Application.LoadLevel ("Multiplayer");
	}
}
