using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	public enum defenseCost
	{
		DEF_CANNON  = 15,
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

	public GameObject infoPanel;
	public GameObject pauseScreen;
	private GameObject resultPanel;
	private GameObject winningScreen;
	private GameObject losingScreen;
	private float timeElapsed;
	private int defenseUsed;
	private int defenseDeleted;
	public int level = 1;
	public Text resourceText;
	public Text levelText;
	public Text enemyLeftText;
	public GameObject Core;
	public EnemyWaves enemyWaves;
	public Enemy enemy;
	public bool isPause = false;
	public bool mouseOverTile = false;
	public bool mouseOverPanel = false;
	public bool mouseOverPauseScreen = false;
	public bool wingame = false;
	public bool losegame = false;

	public int selection = 1;
	public int resources = 200;
	public int enemyLeft;
	//public GameObject tutorial;

	private bool tutorialdone;

	public GameObject defense_1;
	public GameObject defense_2;
	public GameObject defense_3;
	public GameObject defense_4;
	public GameObject defense_5;

	Vector2 InputPos;

	// Use this for initialization
	void Awake () {
		//tutorialdone = tutorial.GetComponent<Tutorial>().tutorialover;
		level = PlayerPrefs.GetInt ("level", 1);
		timeElapsed = 0.0f;
		defenseUsed = 0;
		defenseDeleted = 0;
		enemyLeft = enemyWaves.levels[level - 1].TotalEnemies();
		resourceText.text = "Resources: " + resources;
		levelText.text = "Level: " + level;
		enemyLeftText.text = "Enemy Left: " + enemyLeft;
		infoPanel = GameObject.Find ("Info panel");
		resultPanel = GameObject.Find ("ResultScreen");
		winningScreen = GameObject.Find ("WinningScreen");
		losingScreen = GameObject.Find ("LosingScreen");
		pauseScreen = GameObject.Find ("PauseScreen");

		infoPanel.GetComponent<InfoPanelScript> ().DisablePanel ();
		winningScreen.GetComponent<WinningScreen>().DisablePanel();	
		losingScreen.GetComponent<LosingScreen>().DisablePanel();
		pauseScreen.GetComponent<PauseScreen>().DisablePanel();
	}
	
	// Update is called once per frame
	void Update () {
		timeElapsed += Time.deltaTime;
		resourceText.text = "Resources: " + resources;
		enemyLeftText.text = "Enemy Left: " + enemyLeft;
		CheckIfAffordable();
		CheckForResult();
		if (wingame || losegame) {
			PopUpResult();
		}
		InputPos = new Vector2 (0, 0);

		if (isPause == true) {
			Time.timeScale = 0.0f;
		} else
			Time.timeScale = 1.0f;

		#if UNITY_ANDROID
		if (Input.touchCount > 0) {
			InputPos = Input.GetTouch(0).position;
			if (!mouseOverTile && !mouseOverPanel) {
				DisableInfoPanel();
			}
		}
		#elif UNITY_EDITOR
		if (Input.GetMouseButtonDown(0) && (!mouseOverTile && !mouseOverPanel)) {
			DisableInfoPanel();
		}

		#endif
	}

	public void SetPause() {
		isPause = !isPause;
		EnablePauseScreen ();
	}

	public void SetResume()
	{
		isPause = !isPause;
		DisablePauseScreen ();
	}

	public void SetRestart()
	{
		DisablePauseScreen ();
		Application.LoadLevel("Game");
	}

	public void LevelSelectButton(){
		Application.LoadLevel ("Level Select");
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
		winningScreen.GetComponent<WinningScreen>().SetGameTime (timeElapsed);
		winningScreen.GetComponent<WinningScreen>().SetDefenseUsed (defenseUsed);
		winningScreen.GetComponent<WinningScreen>().SetDefenseDeleted (defenseDeleted);
	}

	public void EnableLosingScreen() {
		losingScreen.GetComponent<LosingScreen>().EnablePanel();
		losingScreen.GetComponent<LosingScreen>().SetGameTime (timeElapsed);
		losingScreen.GetComponent<LosingScreen>().SetDefenseUsed (defenseUsed);
		losingScreen.GetComponent<LosingScreen>().SetDefenseDeleted (defenseDeleted);
	}

	public void EnablePauseScreen() {
		pauseScreen.GetComponent<PauseScreen>().EnablePanel();
	}

	public void DisablePauseScreen() {
		pauseScreen.GetComponent<PauseScreen>().DisablePanel ();
	}
	
	public void SetMouseOverPauseScreen (bool isDone) {
		mouseOverPauseScreen = isDone;
	}

	public void IncreaseDefenseUsed() {
		defenseUsed++;
	}

	public void IncreaseDefenseDeleted() {
		defenseDeleted++;
	}

	void CheckIfAffordable() {
		if (resources - (int)defenseCost.DEF_CANNON < 0) {
			defense_1.GetComponent<Button>().interactable = false;
		} else {
			defense_1.GetComponent<Button>().interactable = true;
		}

		if (resources - (int)defenseCost.DEF_TURRET < 0) {
			defense_2.GetComponent<Button>().interactable = false;
		} else {
			defense_2.GetComponent<Button>().interactable = true;
		}

		if (resources - (int)defenseCost.DEF_SLOW < 0) {
			defense_3.GetComponent<Button>().interactable = false;
		} else {
			defense_3.GetComponent<Button>().interactable = true;
		}

		if (resources - (int)defenseCost.DEF_ANTIAIR < 0) {
			defense_4.GetComponent<Button>().interactable = false;
		} else {
			defense_4.GetComponent<Button>().interactable = true;
		}

		if (resources - (int)defenseCost.DEF_FLAME < 0) {
			defense_5.GetComponent<Button>().interactable = false;
		} else {
			defense_5.GetComponent<Button>().interactable = true;
		}
	}

	void CheckForResult() {
		if (enemyLeft <= 0) {
			if (!wingame) {
				EnableWinningScreen ();
				wingame = true;
			}
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
}
