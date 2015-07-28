using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	public void PlayButton(){
		Application.LoadLevel("Level Select");
	}

	public void CTCButton(){
		Application.LoadLevel("Level Select");
	}

	public void SettingsButton(){
		Application.LoadLevel("Settings");
	}
	
	public void SettingsBackButton(){
		Application.LoadLevel("Main Menu");
	}

	public void ExitButton(){
		Application.Quit();
	}
}
