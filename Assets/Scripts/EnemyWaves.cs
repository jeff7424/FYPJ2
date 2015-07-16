using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWaves : MonoBehaviour {

	[System.Serializable]
	public class Wave{
		public EnemySubwave[] Subwaves;
		private float elapsedTime = 0.0f;

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
	public struct LevelWave{
		public Wave[] waves;
	}


	public LevelWave[] levels;
}
