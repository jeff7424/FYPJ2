using UnityEngine;
using System.Collections;

public class PowerScript : MonoBehaviour {

	GameObject EnemyParent;
	GameObject[] towers;

	// Use this for initialization
	void Start () {
		EnemyParent = GameObject.Find ("EnemyParent");
		//TowerParent = GameObject.Find ("Pathfinding root");
	}
	
	// Update is called once per frame
	void Update () {
		towers = GameObject.FindGameObjectsWithTag("Defense");
	}

	public void SlowEnemies() {
		
	}

	public void Rage() {
		foreach (GameObject tower in towers) {
			tower.GetComponent<Defense>().Rage (3.0f, 2.0f);
		}
	}
}
