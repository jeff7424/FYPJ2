using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

//	public enum defenseCost
//	{
//		DEF_CANNON  = 15,
//		DEF_TURRET  = 20,
//		DEF_SLOW    = 30,
//		DEF_ANTIAIR = 50,
//		DEF_FLAME	= 30
//	}
//
//	public enum upgradeCost
//	{
//		DEF_CANON   = 5,
//		DEF_TURRET  = 7,
//		DEF_SLOW    = 12,
//		DEF_ANTIAIR = 18,
//		DEF_FLAME	= 12
//	}
//
//	public GameObject infoPanel;
//	public GameObject pauseScreen;
//	private GameObject resultPanel;
//	private GameObject winningScreen;
//	private GameObject losingScreen;
//	private float timeElapsed;
//	private int defenseUsed;
//	private int defenseDeleted;
//	public int level = 1;
//	public Text resourceText;
//	public Text levelText;
//	public Text enemyLeftText;
//	public Text errorMsgText;
//	public GameObject Core;
//	public EnemyWaves enemyWaves;
//	public Enemy enemy;
//	public bool isPause = false;
//	public bool mouseOverTile = false;
//	public bool mouseOverPanel = false;
//	public bool mouseOverPauseScreen = false;
//	public bool wingame = false;
//	public bool losegame = false;
//
//	public int selection = 1;
//	public int resources = 200;
//	public int enemyLeft;
//	//public GameObject tutorial;
//
//	private bool tutorialdone;
//	private float errorMsgDisplayTime;
//
//	public GameObject defense_1;
//	public GameObject defense_2;
//	public GameObject defense_3;
//	public GameObject defense_4;
//	public GameObject defense_5;
//
//	Vector2 InputPos;
//
//	// Use this for initialization
//	void Awake () {
//		//tutorialdone = tutorial.GetComponent<Tutorial>().tutorialover;
//		level = PlayerPrefs.GetInt ("level", 1);
//		timeElapsed = 0.0f;
//		defenseUsed = 0;
//		defenseDeleted = 0;
//		enemyLeft = enemyWaves.levels[level - 1].TotalEnemies();
//		resourceText.text = "Resources: " + resources;
//		levelText.text = "Level: " + level;
//		enemyLeftText.text = "Enemy Left: " + enemyLeft;
//		errorMsgText.enabled = false;
//		errorMsgDisplayTime = 0.0f;
//		infoPanel = GameObject.Find ("Info panel");
//		resultPanel = GameObject.Find ("ResultScreen");
//		winningScreen = GameObject.Find ("WinningScreen");
//		losingScreen = GameObject.Find ("LosingScreen");
//		pauseScreen = GameObject.Find ("PauseScreen");
//
//		infoPanel.GetComponent<InfoPanelScript> ().DisablePanel ();
//		winningScreen.GetComponent<WinningScreen>().DisablePanel();	
//		losingScreen.GetComponent<LosingScreen>().DisablePanel();
//		pauseScreen.GetComponent<PauseScreen>().DisablePanel();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (!isPause) {
//			timeElapsed += Time.deltaTime;
//			CheckIfAffordable();
//			CheckForResult();
//			if (wingame || losegame) {
//				PopUpResult();
//			}
//			if (errorMsgDisplayTime > 0.0f) {
//				errorMsgDisplayTime -= Time.deltaTime;
//			} else {
//				errorMsgDisplayTime = 0.0f;
//				errorMsgText.enabled = false;
//			}
//			resourceText.text = "Resources: " + resources;
//			enemyLeftText.text = "Enemy Left: " + enemyLeft;
//
//			InputPos = new Vector2 (0, 0);
//
//			#if UNITY_ANDROID
//			if (Input.touchCount > 0) {
//				InputPos = Input.GetTouch(0).position;
//				if (!mouseOverTile && !mouseOverPanel) {
//					DisableInfoPanel();
//				}
//			}
//			#elif UNITY_EDITOR
//			if (Input.GetMouseButtonDown(0) && (!mouseOverTile && !mouseOverPanel)) {
//				DisableInfoPanel();
//			}
//
//			#endif
//		}
//	}
//
//	public void SetPause() {
//		isPause = !isPause;
//		if (isPause)
//			EnablePauseScreen ();
//		else
//			DisablePauseScreen ();
//	}
//
//	public void SetRestart()
//	{
//		Application.LoadLevel("Game");
//	}
//
//	public void LevelSelectButton(){
//		Application.LoadLevel ("Level Select");
//	}
//
//	public void SetSelection(int selection) {
//		this.selection = selection;
//	}
//
//	public int GetSelection() {
//		return selection;
//	}
//
//	public void EnableInfoPanel() {
//		infoPanel.GetComponent<InfoPanelScript> ().EnablePanel ();
//	}
//
//	public void DisableInfoPanel() {
//		infoPanel.GetComponent<InfoPanelScript> ().DisablePanel ();
//	}
//
//	public void SetInfoPanelTarget(Defense defense) {
//		infoPanel.GetComponent<InfoPanelScript> ().defense = defense;
//	}
//
//	public void SetMouseOverPanel (bool isOver) {
//		mouseOverPanel = isOver;
//	}
//
//	public void EnableWinningScreen() {
//		winningScreen.GetComponent<WinningScreen>().EnablePanel();
//		winningScreen.GetComponent<WinningScreen>().SetGameTime (timeElapsed);
//		winningScreen.GetComponent<WinningScreen>().SetDefenseUsed (defenseUsed);
//		winningScreen.GetComponent<WinningScreen>().SetDefenseDeleted (defenseDeleted);
//	}
//
//	public void EnableLosingScreen() {
//		losingScreen.GetComponent<LosingScreen>().EnablePanel();
//		losingScreen.GetComponent<LosingScreen>().SetGameTime (timeElapsed);
//		losingScreen.GetComponent<LosingScreen>().SetDefenseUsed (defenseUsed);
//		losingScreen.GetComponent<LosingScreen>().SetDefenseDeleted (defenseDeleted);
//	}
//
//	public void EnablePauseScreen() {
//		pauseScreen.GetComponent<PauseScreen>().EnablePanel();
//	}
//
//	public void DisablePauseScreen() {
//		pauseScreen.GetComponent<PauseScreen>().DisablePanel ();
//	}
//	
//	public void SetMouseOverPauseScreen (bool isDone) {
//		mouseOverPauseScreen = isDone;
//	}
//
//	public void IncreaseDefenseUsed() {
//		defenseUsed++;
//	}
//
//	public void IncreaseDefenseDeleted() {
//		defenseDeleted++;
//	}
//
//	void CheckIfAffordable() {
//		if (resources - (int)defenseCost.DEF_CANNON < 0) {
//			defense_1.GetComponent<Button>().interactable = false;
//		} else {
//			defense_1.GetComponent<Button>().interactable = true;
//		}
//
//		if (resources - (int)defenseCost.DEF_TURRET < 0) {
//			defense_2.GetComponent<Button>().interactable = false;
//		} else {
//			defense_2.GetComponent<Button>().interactable = true;
//		}
//
//		if (resources - (int)defenseCost.DEF_SLOW < 0) {
//			defense_3.GetComponent<Button>().interactable = false;
//		} else {
//			defense_3.GetComponent<Button>().interactable = true;
//		}
//
//		if (resources - (int)defenseCost.DEF_ANTIAIR < 0) {
//			defense_4.GetComponent<Button>().interactable = false;
//		} else {
//			defense_4.GetComponent<Button>().interactable = true;
//		}
//
//		if (resources - (int)defenseCost.DEF_FLAME < 0) {
//			defense_5.GetComponent<Button>().interactable = false;
//		} else {
//			defense_5.GetComponent<Button>().interactable = true;
//		}
//	}
//
//	void CheckForResult() {
//		if (enemyLeft <= 0) {
//			if (!wingame) {
//				EnableWinningScreen ();
//				wingame = true;
//			}
//		}
//	}
//
//	void PopUpResult() {
//		if (resultPanel.transform.position.y < 0.0f) {
//			Vector2 newpos = resultPanel.transform.position;
//			newpos.y += 25.0f * Time.deltaTime;
//			if (newpos.y >= 0.0f)
//				newpos.y = 0.0f;
//			resultPanel.transform.position = newpos;
//		}
//	}
//
//	public void DisplayErrorMsg() {
//		errorMsgText.enabled = true;
//		errorMsgDisplayTime = 3.0f;
//	}

	public enum defenseCost
	{
		DEF_CANNON	= 15,
		DEF_TURRET  = 20,
		DEF_SLOW    = 30,
		DEF_ANTIAIR = 50,
		DEF_FLAME	= 30
	}
	
	public enum upgradeCost
	{
		DEF_CANON   = 5,
		DEF_TURRET  = 7,
		DEF_SLOW    = 12,
		DEF_ANTIAIR = 18,
		DEF_FLAME	= 12
	}

	float timeElapsed;
	bool isPause;
	public bool endGame;
	public int level;
	
	GameObject pauseScreen;

	// Use this for initialization
	void Awake () {
		if (Time.timeScale != 1.0f)
			Time.timeScale = 1.0f;
		timeElapsed = 0.0f;
		isPause = false;
		endGame = false;
		if (Application.loadedLevelName == "Game")
			level = PlayerPrefs.GetInt("level", 1) - 1;
		else
			level = 1;
		pauseScreen = GameObject.Find ("PauseScreen");
		pauseScreen.GetComponent<PauseScreen>().DisablePanel();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isPause || !endGame)
			timeElapsed += Time.deltaTime;
	}
	
	public void SetPause() {
		isPause = !isPause;
		if (isPause)
			EnablePauseScreen();
		else
			DisablePauseScreen();
	}
	
	public bool GetPause() {
		return isPause;
	}
	
	public void SetEndGame(bool newvalue) {
		endGame = newvalue;
	}
	
	public void SetRestart()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
	
	public void LevelSelectButton(){
		Application.LoadLevel ("Level Select");
	}
	
	public float GetTimeElapsed() {
		return timeElapsed;
	}

	public void EnablePauseScreen() {
		pauseScreen.GetComponent<PauseScreen>().EnablePanel();
	}

	public void DisablePauseScreen() {
		pauseScreen.GetComponent<PauseScreen>().DisablePanel ();
	}
}
