using UnityEngine;
using System.Collections;

public class EnemyWaves : MonoBehaviour {
	[System.Serializable]
	public struct Wave{
		public int Normal;
		public int Slow;
		public int Fast;

		public int TotalEnemies{
			get{return Normal+Fast+Slow;}
		}
	}

	public Wave[] waves;
}
