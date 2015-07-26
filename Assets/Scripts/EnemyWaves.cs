using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWaves : MonoBehaviour {

	[System.Serializable]
	public class Wave{
		public List<EnemySubwave> Subwaves = new List<EnemySubwave>();
		private float elapsedTime = 0.0f;

		public Wave(){
			Subwaves.Add(new EnemySubwave());
		}

		public List<int> Update(float dt){
			List<int> list = new List<int>();

			elapsedTime += dt;

			foreach(EnemySubwave subwave in Subwaves){
				if(subwave.isActive()){
					int type = subwave.Update();
					
					//If list doesn't have an element to store the count of the enemy type, create it
					if(type >= 0){
						if(type + 1 > list.Count){
							while (list.Count < type + 1){
								list.Add(0);
							}
						}

						++list[type];
					}
				}
				else{
					//Check if enough time has passed for subwave to activate
					if(elapsedTime >= subwave.activateTime)
						subwave.Activate();
				}
			}

			return list;
		}

		public int TotalEnemies(){
			int total = 0;

			foreach(EnemySubwave subwave in Subwaves){
				total += subwave.getTotalEnemies();
			}

			return total;
		}
	}


	[System.Serializable]
	public class Level{
		public List<Wave> waves = new List<Wave>();
		public bool unlimitedWaves = false;

		//Constructor
		public Level(){
			waves.Add(new Wave());
		}

		public int TotalEnemies() {
			int total = 0;

			foreach(Wave wave in waves) {
				total += wave.TotalEnemies ();
			}

			return total;
		}

		public void generateWave(){
			if(unlimitedWaves){
				Wave newWave = new Wave();
				newWave.Subwaves.Clear();

				foreach(EnemySubwave subwave in waves[waves.Count-1].Subwaves){
					EnemySubwave newSubwave = new EnemySubwave();
					newSubwave.spawnRate = subwave.spawnRate;
					newSubwave.activateTime = subwave.activateTime;

					for(int i = 0; i < subwave.Enemies.Count; ++i){
						if(subwave.Enemies[i] <= 0){
							if(waves.Count > i*2 && i < 6)
								newSubwave.Enemies[i] = (int)Random.Range(1, 4);
						}
						else if(subwave.Enemies[i] < 3)
							newSubwave.Enemies[i] = subwave.Enemies[i]*2;
						else
							newSubwave.Enemies[i] = (int)(subwave.Enemies[i]*Random.Range(1.25f, 1.5f));
					}

					newWave.Subwaves.Add(newSubwave);
				}

				waves.Add(newWave);
			}
		}
	}


	public List<Level> levels = new List<Level>();

	//Constructor
	public EnemyWaves(){
		levels.Add(new Level());
	}
}
