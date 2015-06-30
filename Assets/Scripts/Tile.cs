using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public enum defenseCost
	{
		DEF_CANON   = 30,
		DEF_TURRET  = 35,
		DEF_SLOW    = 50,
		DEF_ANTIAIR = 60,
	}

	public Defense defenses;
	public bool isOccupied = false;
	private int cost = 0;
	private int selection = 1;

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
		GameObject game = GameObject.Find ("Game");
		selection = (int)defenses.GetComponent<Defense>().selection;
		switch (selection) {
		case (1):
			cost = (int)defenseCost.DEF_CANON;
			break;
		case (2):
			cost = (int)defenseCost.DEF_TURRET;
			break;
		case (3):
			cost = (int)defenseCost.DEF_SLOW;
			break;
		case (4):
			cost = (int)defenseCost.DEF_ANTIAIR;
			break;
		}
		if (!isOccupied && game.GetComponent<Game>().resources - cost >= 0) {
			BuildDefense ();
			game.GetComponent<Game>().resources -= cost;
		} else {
			Debug.Log ("Occupied");
		}
	}

	public Vector2 TilePosition() {
		return transform.position;
	}
}
