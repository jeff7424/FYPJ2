using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	public GameObject MainMenuPanel;
	public GameObject SettingsPanel;
	public GameObject HelpPanel;
	public GameObject BackButton;
	public GameObject ExitPanel;
	public Text title;
	public Slider volumeSlider;
	public GameObject TutorialPage1, TutorialPage2, TutorialPage3, TutorialPage4, TutorialPage5, TutorialPage6;
	public Text TutorialText;
	int pageNumber;

	void Start() {
		//MainMenuPanel = GameObject.Find ("Main menu panel");
		//SettingsPanel = GameObject.Find ("Settings panel");
		//BackButton = GameObject.Find ("Back Button");
		DisablePanel(SettingsPanel);
		DisablePanel(HelpPanel);
		DisablePanel(ExitPanel);
		DisableBack ();
		volumeSlider.value = PlayerPrefs.GetFloat ("volume", 1);

		TutorialPage1.SetActive(false);
		TutorialPage2.SetActive(false);
		TutorialPage3.SetActive(false);
		TutorialPage4.SetActive(false);
		TutorialPage5.SetActive(false);
		TutorialPage6.SetActive(false);
	}

	public void PlayButton(){
		Application.LoadLevel("Level Select");
	}

	public void ExitButton(){
		Application.Quit();
	}

	public void RightArrow(){
		switch (pageNumber){
		case 1:
			pageNumber ++;
			break;

		case 2:
			pageNumber++;
			break;

		case 3:
			pageNumber++;
			break;

		case 4:
			pageNumber++;
			break;

		case 5:
			pageNumber++;
			break;
			
		case 6:
			pageNumber = 1;
			break;

		default:
			break;
		}
	}

	public void LeftArrow(){
		switch (pageNumber){
		case 1:
			pageNumber = 6;
			break;
			
		case 2:
			pageNumber--;
			break;
			
		case 3:
			pageNumber--;
			break;

		case 4:
			pageNumber--;
			break;

		case 5:
			pageNumber--;
			break;

		case 6:
			pageNumber--;
			break;

		default:
			break;
		}
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
		else if (go.name == HelpPanel.name)
			title.text = "HELP";
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

	void Update()
	{
		switch (pageNumber){
		case 1:
			TutorialPage1.SetActive(true);
			TutorialPage2.SetActive(false);
			TutorialPage3.SetActive(false);
			TutorialPage4.SetActive(false);
			TutorialPage5.SetActive(false);
			TutorialPage6.SetActive(false);
			TutorialText.text = "Objectives";
			break;
			
		case 2:
			TutorialPage1.SetActive(false);
			TutorialPage2.SetActive(true);
			TutorialPage3.SetActive(false);
			TutorialPage4.SetActive(false);
			TutorialPage5.SetActive(false);
			TutorialPage6.SetActive(false);
			TutorialText.text = "Build Tower";
			break;
			
		case 3:
			TutorialPage1.SetActive(false);
			TutorialPage2.SetActive(false);
			TutorialPage3.SetActive(true);
			TutorialPage4.SetActive(false);
			TutorialPage5.SetActive(false);
			TutorialPage6.SetActive(false);
			TutorialText.text = "Tower Type";
			break;

		case 4:
			TutorialPage1.SetActive(false);
			TutorialPage2.SetActive(false);
			TutorialPage3.SetActive(false);
			TutorialPage4.SetActive(true);
			TutorialPage5.SetActive(false);
			TutorialPage6.SetActive(false);
			TutorialText.text = "Upgrade Tower";
			break;

		case 5:
			TutorialPage1.SetActive(false);
			TutorialPage2.SetActive(false);
			TutorialPage3.SetActive(false);
			TutorialPage4.SetActive(false);
			TutorialPage5.SetActive(true);
			TutorialPage6.SetActive(false);
			TutorialText.text = "Skills";
			break;
			
		case 6:
			TutorialPage1.SetActive(false);
			TutorialPage2.SetActive(false);
			TutorialPage3.SetActive(false);
			TutorialPage4.SetActive(false);
			TutorialPage5.SetActive(false);
			TutorialPage6.SetActive(true);
			TutorialText.text = "Enemy Type";
			break;

		default:
			break;
		}
	}

	public void setPageNoToOne()
	{
		pageNumber = 1;
	}

	public void setPageNoToZero()
	{
		pageNumber = 0;
	}
}
