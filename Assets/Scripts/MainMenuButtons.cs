using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	public GameObject MainMenuPanel;
	public GameObject SettingsPanel;
	public GameObject BackButton;
	public GameObject ExitPanel;
	public GameObject CreditPanel;
	public Text title;
	public Slider volumeSlider;

	void Start() {
		//MainMenuPanel = GameObject.Find ("Main menu panel");
		//SettingsPanel = GameObject.Find ("Settings panel");
		//BackButton = GameObject.Find ("Back Button");
		DisablePanel(SettingsPanel);
		DisablePanel (ExitPanel);
		DisablePanel (CreditPanel);
		DisableBack();
		volumeSlider.value = PlayerPrefs.GetFloat ("volume", 1);
	}

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

	public void DisablePanel(GameObject go) {
		go.GetComponent<Image>().enabled = false;
		for (int i = 0; i < go.transform.childCount; i++) {
			go.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void EnablePanel(GameObject go) {
		go.GetComponent<Image>().enabled = true;
		for (int i = 0; i < go.transform.childCount; i++) {
			go.transform.GetChild(i).gameObject.SetActive(true);
		}
		if (go.name == MainMenuPanel.name)
			title.text = "TENKZ VS ROBOTZ";
		else if (go.name == SettingsPanel.name)
			title.text = "SETTINGS";
		else if (go.name == CreditPanel.name)
			title.text = "CREDITS";
	}

	public void EnableBack() {
		BackButton.GetComponent<Button>().enabled = true;
		BackButton.GetComponent<Image>().enabled = true;
		for (int i = 0; i < BackButton.transform.childCount; i++) {
			BackButton.transform.GetChild(i).gameObject.SetActive(true);
		}
	}

	public void DisableBack() {
		BackButton.GetComponent<Button>().enabled = false;
		BackButton.GetComponent<Image>().enabled = false;
		for (int i = 0; i < BackButton.transform.childCount; i++) {
			BackButton.transform.GetChild(i).gameObject.SetActive(false);
		}
	}
	 
	public void SetVolume() {
		PlayerPrefs.SetFloat("volume", volumeSlider.value);
	}
}
