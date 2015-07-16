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
			if(LevelWaves.levels[level].waves[currWave].TotalEnemies() <= 0){
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

	public int getCurrWave(){return currWave;}
}
