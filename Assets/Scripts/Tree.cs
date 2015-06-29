using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
	private GameObject Obstacle;
	public GameObject obstacleObject;

	// Use this for initialization
	void Start () {

	}
		
	// Update is called once per frame
	void Update () {

	}

	void SpawnTree()
	{
		Obstacle = Instantiate (Obstacle, transform.position, Quaternion.identity) as GameObject;
		Obstacle.transform.parent = transform;
	}
}
