using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawnMaster : MonoBehaviour {

//	[System.Serializable]
//	public class Obstacles {
//		public List<ObstacleSpawn> obstaclePos = new List<ObstacleSpawn>();
//
//		public Obstacles() {
//			obstaclePos.Add (new ObstacleSpawn());
//		}
//	}

//	[System.Serializable]
//	public class Level {
//		public List<ObstacleSpawn> obstacles = new List<ObstacleSpawn>();
//
//		public Level() {
//			obstacles.Add (new ObstacleSpawn());
//		}
//	}

	
	[System.Serializable]
	public class ObstacleType {

		public enum obstacleType 
		{
			OBS_OBSTACLE,
			OBS_PLATFORM,
			OBS_TUNNEL,
			OBS_MAX
		}
		
		public List<ObstacleSpawn> type;
		
		public ObstacleType() {
			type = new List<ObstacleSpawn>();
			for (int i = 0; i < (int)ObstacleType.obstacleType.OBS_MAX; ++i)
				type.Add (new ObstacleSpawn());
		}

		public int GetTotal() {
			int total = 0;

			foreach(ObstacleSpawn number in type){
				total += number.GetTotal();
			}
			
			return total;
		}

		public int GetTotalType() {
			return (int)obstacleType.OBS_MAX;
		}
	}


	public List<ObstacleType> levels = new List<ObstacleType>();
}
