//using UnityEngine;
//using System.Collections;
//
//public class Game_Multiplayer : MonoBehaviour {
//
//	public enum defenseCost
//	{
//		DEF_CANNON	= 15,
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
//	float timeElapsed;
//	bool isPause;
//	bool endGame;
//
//	GameObject pauseScreen;
//
//	// Use this for initialization
//	void Start () {
//		timeElapsed = 0.0f;
//		isPause = false;
//		endGame = false;
//		pauseScreen = GameObject.Find ("PauseScreen");
//		pauseScreen.GetComponent<PauseScreen>().DisablePanel();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (!isPause || !endGame)
//			timeElapsed += Time.deltaTime;
//	}
//
//	public void SetPause() {
//		isPause = !isPause;
//		//if (isPause)
//			// Enable pause
//	}
//
//	public bool GetPause() {
//		return isPause;
//	}
//
//	public void SetEndGame(bool newvalue) {
//		endGame = newvalue;
//	}
//
//	public void SetRestart()
//	{
//		Application.LoadLevel("Multiplayer");
//	}
//	
//	public void LevelSelectButton(){
//		Application.LoadLevel ("Level Select");
//	}
//
//	public float GetTimeElapsed() {
//		return timeElapsed;
//	}
//}
