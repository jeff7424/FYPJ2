using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
	//The nodes where enemies will spawn
	public GameObject[] spawnNodes;

	public GameObject Enemy;

	private Random randomSpawn = new Random();
	private float time = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if(time >= 1.0f){
			Instantiate(Enemy, spawnNodes[Random.Range(0, spawnNodes.Length)].transform.position, Quaternion.identity);
			time = 0.0f;
		}
	}
}
