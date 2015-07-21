using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ObstacleSpawn{

	public List<int> Obstacles;

	public ObstacleSpawn() {
		Obstacles = new List<int>();
		for (int i = 0; i < (int)Node.NodeType.NODE_MAX; ++i)
			Obstacles.Add (0);
	}
}