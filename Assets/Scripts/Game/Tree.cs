using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
	private GameObject Obstacle;
	public GameObject obstacleObject;

	public enum obstacleType {
		TYPE_NONE,
		TYPE_TREE,
		TYPE_TUNNEL,
		TYPE_PLATFORM,
		TYPE_MAX
	}

	// Use this for initialization
	void Start () {

	}
		
	// Update is called once per frame
	void Update () {

	}

	void SpawnTree() {
		Obstacle = Instantiate (Obstacle, transform.position, Quaternion.identity) as GameObject;
		Obstacle.transform.parent = transform;
	}
}
