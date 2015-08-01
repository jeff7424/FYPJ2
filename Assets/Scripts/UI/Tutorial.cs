using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Tutorial : MonoBehaviour {

	public bool tutorialover;

	// Use this for initialization
	void Start () {
		LoadTutorial();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveTutorial() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/Tutorial");
		TutorialOver t = new TutorialOver();
		t.TutorialDone = tutorialover;
		bf.Serialize(file, t);
		file.Close();
	}

	public void LoadTutorial() {
		if (File.Exists (Application.persistentDataPath + "/Tutorial")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/Tutorial", FileMode.Open);
			TutorialOver t = (TutorialOver)bf.Deserialize(file);
			file.Close();
			tutorialover = t.TutorialDone;
		}
	}
}

[Serializable]
public class TutorialOver {
	public bool TutorialDone;
}
