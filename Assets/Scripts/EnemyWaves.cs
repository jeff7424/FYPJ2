using UnityEngine;
using System.Collections;

public class EnemyWaves : MonoBehaviour {
	[System.Serializable]
	public struct Wave{
		public int Normal;
		public int Slow;
		public int Fast;
		public int Jump;

		public int TotalEnemies{
			get{return Normal+Fast+Slow+Jump;}
		}
	}
	
	[System.Serializable]
	public struct LevelWave{
		public Wave[] waves;
	}
	
	//public Wave[] waves;
	public LevelWave[] levels;
}
