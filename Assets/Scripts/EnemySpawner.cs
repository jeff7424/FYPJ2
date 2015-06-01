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

	private Random randomSpawn = new Random();
	private float time = 0.0f;
	private float waveChangeTimer = 0.0f;

	private int currWave = 0;

	// Use this for initialization
	void Start () {
		WaveText.text = "Wave 1";
	}
	
	// Update is called once per frame
	void Update () {
//		time += Time.deltaTime;
//		if (time >= 1.0f) {
//			GameObject newEnemy = (GameObject)Instantiate (Enemy, spawnNodes [Random.Range (0, spawnNodes.Length)].transform.position, Quaternion.identity);
//			newEnemy.transform.SetParent (EnemyParent.transform);
//			newEnemy.GetComponent<Enemy> ().setType ((global::Enemy.enemyType)Random.Range (0, (int)global::Enemy.enemyType.TYPE_MAX));
//			//newEnemy.GetComponent<Enemy>().setType(global::Enemy.enemyType.TYPE_SLOW);
//			time = 0.0f;
//		}


		if(currWave < LevelWaves.waves.Length){
			if(LevelWaves.waves[currWave].TotalEnemies <= 0){
				//When wave has finished spawning all enemies
				//Wait for timer before going to next wave
				waveChangeTimer += Time.deltaTime;
				if(waveChangeTimer > 10.0f && currWave+1 < LevelWaves.waves.Length){
					++currWave;
					WaveText.text = "Wave " + (currWave+1);
					waveChangeTimer = 0.0f;
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
							if(LevelWaves.waves[currWave].Normal > 0){
								--LevelWaves.waves[currWave].Normal;
								canSpawn = true;
							}
							else
								canSpawn = false;
							break;

						case 1:
							if(LevelWaves.waves[currWave].Fast > 0){
								--LevelWaves.waves[currWave].Fast;
								canSpawn = true;
							}
							else
								canSpawn = false;
							break;

						case 2:
							if(LevelWaves.waves[currWave].Slow > 0){
								--LevelWaves.waves[currWave].Slow;
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
