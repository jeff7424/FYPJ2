using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	//The nodes where enemies will spawn
	public GameObject game;
	public GameObject[] spawnNodes;

	public GameObject EnemyParent;
	public GameObject Enemy;
	public EnemyWaves LevelWaves;
	public Text WaveText;

	private float waveChangeTimer = 0.0f;

	private int level = 0;
	private int currWave = 0;

	// Use this for initialization
	void Start () {
		WaveText.text = "Wave 1";
		if (Application.loadedLevelName == "Game")
			level = game.GetComponent<Game>().level;
		else
			level = 1;

		Debug.Log (level);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (LevelWaves.levels[level].TotalEnemies());
		if (!game.GetComponent<Game>().GetPause () && !game.GetComponent<Game>().endGame) {
			if(currWave < LevelWaves.levels[level].waves.Count){

				//If the level has unlimited wsves, create more
				if(currWave == LevelWaves.levels[level].waves.Count - 1 &&
				   LevelWaves.levels[level].unlimitedWaves){
					LevelWaves.levels[level].generateWave();
				}

				if(LevelWaves.levels[level].waves[currWave].TotalEnemies() <= 0){
					//When wave has finished spawning all enemies
					//Wait for timer before going to next wave
					waveChangeTimer += Time.deltaTime;
					if(waveChangeTimer > 10.0f){
						++currWave;
						WaveText.text = "Wave " + (currWave+1);
						waveChangeTimer = 0.0f;
					} else if (currWave == LevelWaves.levels[level].waves.Count-1) {
						//Victory!
	//					GameObject win = GameObject.Find ("WinGame");
	//					win.GetComponent<Text>().enabled = true;
					}
				}
				else{
					List<int> spawnList = LevelWaves.levels[level].waves[currWave].Update(Time.deltaTime);
					
					if(spawnList.Count > 0){
						for(int index = 0; index < spawnList.Count; ++index){
							for(int i = 0; i < spawnList[index]; ++i){
								GameObject newEnemy = (GameObject)Instantiate (Enemy, spawnNodes [Random.Range (0, spawnNodes.Length)].transform.position, Quaternion.identity);
								newEnemy.transform.SetParent (EnemyParent.transform);
								newEnemy.GetComponent<Enemy> ().setType ((global::Enemy.enemyType)index);
							}
						}
					}
				}
			}
		}
	}

	public int getCurrWave(){return currWave;}

	public GameObject spawnEnemy(Enemy.enemyType type){
		GameObject newEnemy = (GameObject)Instantiate(Enemy, spawnNodes [Random.Range (0, spawnNodes.Length)].transform.position, Quaternion.identity);
		newEnemy.GetComponent<Enemy>().setType(type);
		newEnemy.transform.SetParent (EnemyParent.transform);
		return newEnemy;
	}
}
