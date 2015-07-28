using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

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
		DEF_CANNON   = 5,
		DEF_TURRET  = 7,
		DEF_SLOW    = 12,
		DEF_ANTIAIR = 18,
		DEF_FLAME	= 12
	}

	float timeElapsed;
	bool isPause;
	public bool startGame;
	public bool endGame;
	public int level;
	
	GameObject pauseScreen;
	Text countdowntext;
	Text player1text;
	Text player2text;
	float countdowntimer;

	// Use this for initialization
	void Awake () {
		if (Time.timeScale != 1.0f)
			Time.timeScale = 1.0f;
		timeElapsed = 0.0f;
		isPause = false;
		endGame = false;
		startGame = false;
		if (Application.loadedLevelName == "Game")
			level = PlayerPrefs.GetInt("level", 1) - 1;
		else
			level = 1;
		pauseScreen = GameObject.Find ("PauseScreen");
		pauseScreen.GetComponent<PauseScreen>().DisablePanel();
		countdowntimer = 4.0f;
		countdowntext = GameObject.Find ("Timer").GetComponent<Text>();
		if (Application.loadedLevelName == "Multiplayer") {
			player1text = GameObject.Find ("Player 1 Text").GetComponent<Text>();
			player2text = GameObject.Find ("Player 2 Text").GetComponent<Text>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (countdowntimer > 0.0f) {
			countdowntimer -= Time.deltaTime;
			startGame = false;
			isPause = true;
			int temp = (int)countdowntimer;
			if (countdowntimer % 1 < 1 * Time.deltaTime) {
				if (countdowntimer < 1.0f) {
					GetComponent<AudioSource>().pitch = 2.0f;
				}
				GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
			}
			countdowntext.text = temp.ToString();
			if (temp <= 0) {
				countdowntext.text = "GO!";
			}
		}
		else {
			if (!startGame) {
				startGame = true;
				isPause = false;
				countdowntext.enabled = false;
				if (Application.loadedLevelName == "Multiplayer") {
					player1text.enabled = false;
					player2text.enabled = false;
				}
			}
		}
		if (!isPause && !endGame && startGame) {
			timeElapsed += Time.deltaTime;
		}
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
