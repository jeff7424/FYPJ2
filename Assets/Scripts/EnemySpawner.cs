using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	//The nodes where enemies will spawn
	public GameObject[] spawnNodes;

	public GameObject EnemyParent;
	public GameObject Enemy;
	public EnemyWaves LevelWaves;
	public Text WaveText;

	private float time = 0.0f;
	private float waveChangeTimer = 0.0f;

	private int level = 0;
	private int currWave = 0;

	// Use this for initialization
	void Start () {
		WaveText.text = "Wave 1";
		level = GlobalVariables.level - 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(currWave <= LevelWaves.levels[level].waves.Length){
			if(LevelWaves.levels[level].waves[currWave].TotalEnemies <= 0){
				//When wave has finished spawning all enemies
				//Wait for timer before going to next wave
				waveChangeTimer += Time.deltaTime;
				if(waveChangeTimer > 10.0f && currWave < 5){
					++currWave;
					WaveText.text = "Wave " + (currWave+1);
				} else if (currWave == 5) {
					GameObject win = GameObject.Find ("WinGame");
					win.GetComponent<Text>().enabled = true;
				}
			}
			else{
				time += Time.deltaTime;
				if (time >= 1.0f) {
					bool canSpawn = false;
					int spawnType = 0;

					while(!canSpawn){
						spawnType = Random.Range (0, (int)global::Enemy.enemyType.TYPE_MAX);

						switch(spawnType){
						case 0:
							if(LevelWaves.levels[level].waves[currWave].Normal > 0){
								--LevelWaves.levels[level].waves[currWave].Normal;
								canSpawn = true;
							}
							else
								canSpawn = false;
							break;

						case 1:
							if(LevelWaves.levels[level].waves[currWave].Fast > 0){
								--LevelWaves.levels[level].waves[currWave].Fast;
								canSpawn = true;
							}
							else
								canSpawn = false;
							break;

						case 2:
							if(LevelWaves.levels[level].waves[currWave].Slow > 0){
								--LevelWaves.levels[level].waves[currWave].Slow;
								canSpawn = true;
							}
							else
								canSpawn = false;
							break;
							
						case 3:
							if(LevelWaves.levels[level].waves[currWave].Jump > 0){
								--LevelWaves.levels[level].waves[currWave].Jump;
								canSpawn = true;
							}
							else
								canSpawn = false;
							break;
						}
					}

					//Spawn new enemy
					GameObject newEnemy = (GameObject)Instantiate (Enemy, spawnNodes [Random.Range (0, spawnNodes.Length)].transform.position, Quaternion.identity);
					newEnemy.transform.SetParent (EnemyParent.transform);
					newEnemy.GetComponent<Enemy> ().setType ((global::Enemy.enemyType)spawnType);
					time = 0.0f;
				}
			}
		}
	}

	public int getCurrWave(){return currWave;}
}
