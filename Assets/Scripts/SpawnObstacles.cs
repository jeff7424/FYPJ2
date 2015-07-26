using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour {

	[System.Serializable]
	public class SpawnObs
	{
		public int level;
		public int[] TreePos;
		public int[] PlatformPos;
		public int[] TunnelPos;
	}
	
	public List<SpawnObs> levels = new List<SpawnObs>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
