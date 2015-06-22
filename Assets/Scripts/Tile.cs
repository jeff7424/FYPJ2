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

		//Update pathNode type and search path for all AI present
		GetComponent<Node>().type = Node.NodeType.NODE_TOWER;
		EnemyMovementAI[] enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
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
