using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObstacleSpawn{
	
	public List<int> quantity;

	public ObstacleSpawn() {
		quantity = new List<int>();
		quantity.Add (0);
	}

	public int GetTotal () {
		int total = 0;
		
		foreach(int number in quantity){
			total = number;
		}
		
		return total;
	}
}

[System.Serializable]
public class ObstaclePos {
//	public List<int> index;
//
//	public ObstaclePos() {
//		index = new List<int>();
//		index.Add(0);
//	}	
//
//	public int returnIndex() {
//		int total = 0;
//		
//		foreach(int number in index){
//			total = number;
//		}
//		
//		return total;
//	}
	public int index;
}