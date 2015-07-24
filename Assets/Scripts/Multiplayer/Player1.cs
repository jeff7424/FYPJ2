using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player1 : MonoBehaviour {

	GameObject game;
	public GameObject infoPanel;
	GameObject resultPanel;
	GameObject winningScreen;
	GameObject losingScreen;
	GameObject button_defense1;
	GameObject button_defense2;
	GameObject button_defense3;
	GameObject button_defense4;
	GameObject button_defense5;

	GameObject resourceText;
	GameObject errorMsgText;
	GameObject enemyLeftText;
	
	GameObject enemyWaves;

	public int enemyLeft;
	int defenseUsed;
	int defenseDeleted;
	public int selection;
	public int resources;

	float errorMsgDisplayTime;

	public bool mouseOverTile;
	public bool mouseOverPanel;
	public bool wingame;
	public bool losegame;

	// Use this for initialization
	void Awake () {
		game = GameObject.Find ("Game");
		if (Application.loadedLevelName == "Game") {
			infoPanel = GameObject.Find ("Info panel");
			resultPanel = GameObject.Find ("ResultScreen");
			winningScreen = GameObject.Find ("WinningScreen");
			losingScreen = GameObject.Find ("LosingScreen");
			button_defense1 = GameObject.Find ("Defense 1");
			button_defense2 = GameObject.Find ("Defense 2");
			button_defense3 = GameObject.Find ("Defense 3");
			button_defense4 = GameObject.Find ("Defense 4");
			button_defense5 = GameObject.Find ("Defense 5");
			
			resourceText = GameObject.Find ("Resources");
			errorMsgText = GameObject.Find ("ErrorMsg");
			enemyLeftText = GameObject.Find ("Enemies Left");
			
			enemyWaves = GameObject.Find ("EnemyWaves");

			enemyLeft = enemyWaves.GetComponent<EnemyWaves>().levels[game.GetComponent<Game>().level - 1].TotalEnemies();
			enemyLeftText.GetComponent<Text>().text = "Enemy Left: " + enemyLeft;
		} else if (Application.loadedLevelName == "Multiplayer") {
			if (gameObject.tag == "Player 1") {
				infoPanel = GameObject.Find ("Info panel 1");
				resultPanel = GameObject.Find ("ResultScreen 1");
				winningScreen = GameObject.Find ("WinningScreen 1");
				losingScreen = GameObject.Find ("LosingScreen 1");
				button_defense1 = GameObject.Find ("Defense 1 1");
				button_defense2 = GameObject.Find ("Defense 2 1");
				button_defense3 = GameObject.Find ("Defense 3 1");
				button_defense4 = GameObject.Find ("Defense 4 1");
				button_defense5 = GameObject.Find ("Defense 5 1");

				resourceText = GameObject.Find ("Resources 1");
				errorMsgText = GameObject.Find ("ErrorMsg 1");

				enemyWaves = GameObject.Find ("EnemyWaves 1");

			} else if (gameObject.tag == "Player 2") {
				infoPanel = GameObject.Find ("Info panel 2");
				resultPanel = GameObject.Find ("ResultScreen 2");
				winningScreen = GameObject.Find ("WinningScreen 2");
				losingScreen = GameObject.Find ("LosingScreen 2");
				button_defense1 = GameObject.Find ("Defense 1 2");
				button_defense2 = GameObject.Find ("Defense 2 2");
				button_defense3 = GameObject.Find ("Defense 3 2");
				button_defense4 = GameObject.Find ("Defense 4 2");
				button_defense5 = GameObject.Find ("Defense 5 2");
				
				resourceText = GameObject.Find ("Resources 2");
				errorMsgText = GameObject.Find ("ErrorMsg 2");
				
				enemyWaves = GameObject.Find ("EnemyWaves 2");
			}
		}
		defenseUsed = 0;
		defenseDeleted = 0;
		selection = 0;
		resources = 200;

		resourceText.GetComponent<Text>().text = "Resources: " + resources;
		errorMsgText.GetComponent<Text>().enabled = false;
		errorMsgDisplayTime = 0.0f;

		mouseOverTile = false;
		mouseOverPanel = false;
		wingame = false;
		losegame = false;

		infoPanel.GetComponent<InfoPanelScript>().DisablePanel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!game.GetComponent<Game>().GetPause ()) {
			CheckIfAffordable();
			if (wingame || losegame)
				PopUpResult ();
			if (errorMsgDisplayTime > 0.0f)
				errorMsgDisplayTime -= Time.deltaTime;
			else {
				errorMsgDisplayTime = 0.0f;
				errorMsgText.GetComponent<Text>().enabled = false;
			}
			resourceText.GetComponent<Text>().text = "Resources: " + resources;
			if (Application.loadedLevelName == "Game")
				enemyLeftText.GetComponent<Text>().text = "Enemy Left: " + enemyLeft;

		}
	}

	public void SetSelection(int selection) {
		this.selection = selection;
	}
	
	public int GetSelection() {
		return selection;
	}
	
	public void EnableInfoPanel() {
		infoPanel.GetComponent<InfoPanelScript> ().EnablePanel ();
	}
	
	public void DisableInfoPanel() {
		infoPanel.GetComponent<InfoPanelScript> ().DisablePanel ();
	}
	
	public void SetInfoPanelTarget(Defense defense) {
		infoPanel.GetComponent<InfoPanelScript> ().defense = defense;
	}
	
	public void SetMouseOverPanel (bool isOver) {
		mouseOverPanel = isOver;
	}
	
	public void EnableWinningScreen() {
		winningScreen.GetComponent<WinningScreen>().EnablePanel();
		winningScreen.GetComponent<WinningScreen>().SetGameTime (game.GetComponent<Game>().GetTimeElapsed());
		winningScreen.GetComponent<WinningScreen>().SetDefenseUsed (defenseUsed);
		winningScreen.GetComponent<WinningScreen>().SetDefenseDeleted (defenseDeleted);
	}
	
	public void EnableLosingScreen() {
		losingScreen.GetComponent<LosingScreen>().EnablePanel();
		losingScreen.GetComponent<LosingScreen>().SetGameTime (game.GetComponent<Game>().GetTimeElapsed());
		losingScreen.GetComponent<LosingScreen>().SetDefenseUsed (defenseUsed);
		losingScreen.GetComponent<LosingScreen>().SetDefenseDeleted (defenseDeleted);
	}
	
	public void IncreaseDefenseUsed() {
		defenseUsed++;
	}
	
	public void IncreaseDefenseDeleted() {
		defenseDeleted++;
	}
	
	void CheckIfAffordable() {
		if (resources - (int)Game.defenseCost.DEF_CANNON < 0) {
			button_defense1.GetComponent<Button>().interactable = false;
		} else {
			button_defense1.GetComponent<Button>().interactable = true;
		}
		
		if (resources - (int)Game.defenseCost.DEF_TURRET < 0) {
			button_defense2.GetComponent<Button>().interactable = false;
		} else {
			button_defense2.GetComponent<Button>().interactable = true;
		}
		
		if (resources - (int)Game.defenseCost.DEF_SLOW < 0) {
			button_defense3.GetComponent<Button>().interactable = false;
		} else {
			button_defense3.GetComponent<Button>().interactable = true;
		}
		
		if (resources - (int)Game.defenseCost.DEF_ANTIAIR < 0) {
			button_defense4.GetComponent<Button>().interactable = false;
		} else {
			button_defense4.GetComponent<Button>().interactable = true;
		}
		
		if (resources - (int)Game.defenseCost.DEF_FLAME < 0) {
			button_defense5.GetComponent<Button>().interactable = false;
		} else {
			button_defense5.GetComponent<Button>().interactable = true;
		}
	}
	
	void PopUpResult() {
		if (resultPanel.transform.position.y < 0.0f) {
			Vector2 newpos = resultPanel.transform.position;
			newpos.y += 25.0f * Time.deltaTime;
			if (newpos.y >= 0.0f)
				newpos.y = 0.0f;
			resultPanel.transform.position = newpos;
		}
	}
	
	public void DisplayErrorMsg() {
		errorMsgText.GetComponent<Text>().enabled = true;
		errorMsgDisplayTime = 3.0f;
	}

//	void ButtonUpdate() {
//		if (selection == 1) {
//			button_defense1.GetComponent<Image>().sprite = buttonSprite[0];
//		} else {
//			button_defense1.GetComponent<Image>().sprite = buttonSprite[1];
//		}
//		
//		if (selection == 2) {
//			button_defense2.GetComponent<Image>().sprite = buttonSprite[0];
//		} else {
//			button_defense2.GetComponent<Image>().sprite = buttonSprite[1];
//		}
//		
//		if (selection == 3) {
//			button_defense3.GetComponent<Image>().sprite = buttonSprite[0];
//		} else {
//			button_defense3.GetComponent<Image>().sprite = buttonSprite[1];
//		}
//		
//		if (selection == 4) {
//			button_defense4.GetComponent<Image>().sprite = buttonSprite[0];
//		} else {
//			button_defense4.GetComponent<Image>().sprite = buttonSprite[1];
//		}
//		
//		if (selection == 5) {
//			button_defense5.GetComponent<Image>().sprite = buttonSprite[0];
//		} else {
//			button_defense5.GetComponent<Image>().sprite = buttonSprite[1];
//		}
//	}
}
