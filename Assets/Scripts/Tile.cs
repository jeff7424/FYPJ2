using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Defense defenses;
	private bool isOccupied = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void BuildDefense() {
		// Build tower when button is released
		Defense defense = (Defense)Instantiate (defenses);
		defense.transform.position = transform.position;
		isOccupied = true;
		Debug.Log ("Defense built");
		this.transform.parent = null;
		GameObject.Find("Pathfinder").GetComponent<AstarPath>().Scan();
		EnemyMovementAI[] enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.FindPath();
		}
	}

	void OnMouseDown() {
		if (!isOccupied) {
			BuildDefense ();
		} else {
			Debug.Log ("Occupied");
		}
	}

	public Vector2 TilePosition() {
		return transform.position;
	}
}
