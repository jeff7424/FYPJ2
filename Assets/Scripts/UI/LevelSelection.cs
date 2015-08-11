using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelSelection : MonoBehaviour {

	public int level;
	private bool tutorial;
	
	public GameObject Levels;
	public GameObject LevelSelectPage1, LevelSelectPage2, LevelSelectPage3, LevelSelectPage4, LevelSelectPage5, LevelSelectPage6, LevelSelectPage7, LevelSelectPage8, LevelSelectPage9, LevelSelectPage10, LevelSelectPage11;
	public Text LevelSelectText;
	public Text title;
	int pageNumber;

	// Use this for initialization
	void Start () {
		level = 0;
		pageNumber = 1;
		tutorial = false;
		PlayerPrefs.SetInt ("level", level);
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
			pageNumber++;
			break;

		case 7:
			pageNumber++;
			break;

		case 8:
			pageNumber++;
			break;

		case 9:
			pageNumber++;
			break;
			
		case 10:
			pageNumber++;
			break;

		case 11:
			pageNumber = 1;
			break;
			
		default:
			break;
		}
	}

	public void LeftArrow(){
		switch (pageNumber){
		case 1:
			pageNumber = 11;
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
			
		case 7:
			pageNumber--;
			break;
			
		case 8:
			pageNumber--;
			break;
			
		case 9:
			pageNumber--;
			break;
			
		case 10:
			pageNumber--;
			break;
			
		case 11:
			pageNumber--;
			break;

		default:
			break;
		}
	}

	void Update()
	{
		switch (pageNumber){
		case 1:
			LevelSelectPage1.SetActive(true);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 1";
			break;
			
		case 2:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(true);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 2";
			break;
			
		case 3:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(true);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 3";
			break;
			
		case 4:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(true);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 4";
			break;
			
		case 5:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(true);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 5";
			break;
			
		case 6:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(true);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 6";
			break;
			
		case 7:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(true);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 7";
			break;
			
		case 8:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(true);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 8";
			break;
			
		case 9:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(true);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 9";
			break;
			
		case 10:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(true);
			LevelSelectPage11.SetActive(false);
			LevelSelectText.text = "Level 10";
			break;

		case 11:
			LevelSelectPage1.SetActive(false);
			LevelSelectPage2.SetActive(false);
			LevelSelectPage3.SetActive(false);
			LevelSelectPage4.SetActive(false);
			LevelSelectPage5.SetActive(false);
			LevelSelectPage6.SetActive(false);
			LevelSelectPage7.SetActive(false);
			LevelSelectPage8.SetActive(false);
			LevelSelectPage9.SetActive(false);
			LevelSelectPage10.SetActive(false);
			LevelSelectPage11.SetActive(true);
			LevelSelectText.text = "Multiplayer";
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
	}

	public void StartLevel(int newValue) {
		level = newValue;
		PlayerPrefs.SetInt ("level", level);
		Load();
		Application.LoadLevel ("Game");
	}

	public void StartMultiplayer() {
		Application.LoadLevel ("Multiplayer");
	}

	public void BackToMainMenu() {
		Application.LoadLevel ("Main Menu");
	}
}
