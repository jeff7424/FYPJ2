using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public enum defenseCost
	{
		DEF_CANON   = 3,
		DEF_TURRET  = 3,
		DEF_SLOW    = 5,
		DEF_ANTIAIR = 6,
	}

	public Defense defenses;
	private Defense defense;
	private GameObject game;
	public bool isOccupied = false;
	public bool isSelected = false;
	public bool deleteDefense = false;
	private int cost = 0;
	private int selection = 1;
	public Tree trees;
	public Sprite theBase;
	public Sprite tile;
	public Sprite weapon;
	public Texture2D current;
	public Texture2D[] weapontype;
	public Rect temp;
	private GameObject info_panel;

	// Use this for initialization
	void Start () {
		//isOccupied = false;
		info_panel = GameObject.Find ("Info panel");
		info_panel.GetComponent<Image> ().enabled = false;
		for (int i = 0; i < info_panel.transform.childCount; i++) {
			info_panel.transform.GetChild(i).gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void BuildDefense() {
		// Build tower when button is released
		defense = (Defense)Instantiate (defenses);
		defense.transform.position = transform.position;
		defense.transform.SetParent (gameObject.transform);
		isOccupied = true;
		Debug.Log ("Defense built");

		//Update pathNode type and search path for all AI present
		GetComponent<Node>().type = Node.NodeType.NODE_TOWER;
		EnemyMovementAI[] enemyAIs = GameObject.Find("EnemyParent").GetComponentsInChildren<EnemyMovementAI>();
		foreach(EnemyMovementAI ai in enemyAIs){
			ai.searchPath();
		}
	}

	public void DestroyDefense() {
		Destroy (defense.gameObject);
		isOccupied = false;
		deleteDefense = false;
		selection = 1;
	}

	public void RankUpDefense() {
		defense.GetComponent<Defense> ().SetRank (defense.GetComponent<Defense> ().GetRank () + 1);
	}

	void RenderGhost() {
		//selection = (int)defenses.GetComponent<Defense>().selection;
		//current = weapontype [selection - 1];
		//weapon = Sprite.Create (current, new Rect (0, 0, current.width, current.height), new Vector2(0.5f, 0.5f));

		//this.GetComponent<SpriteRenderer> ().sprite = weapon;
		this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);

		//temp = new Rect (0, 0, current.width, current.height);
	}

	void OnMouseEnter() {
		RenderGhost ();
	}

	void OnMouseExit() {
		// Revert back to original sprite
		//this.GetComponent<SpriteRenderer> ().sprite = tile;
		this.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
	}

	void OnMouseDown() {
		GameObject game = GameObject.Find ("Game");
		selection = game.GetComponent<Game>().selection;
		if (selection == 0) {
			deleteDefense = true;
		} else {
			deleteDefense = false;
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
		}
		
		if (!isOccupied && game.GetComponent<Game> ().resources - cost >= 0 && !deleteDefense && !isSelected) {
			if (!checkAIPath ())
				Debug.Log ("Monsters cannot pass through");
			else {
				BuildDefense ();
				game.GetComponent<Game> ().resources -= cost;
			}
		} else {
			if (isOccupied && deleteDefense) {
				DestroyDefense ();
			} else if (isOccupied && !deleteDefense && !isSelected) {
				// level up defense
				isSelected = true;
//				GameObject info_panel = GameObject.Find ("Info panel");
//				info_panel.GetComponent<InfoPanelScript>().defense = defense;
				DisplayInfo ();

			}
		}
	}

	public void DisplayInfo() {
		info_panel.GetComponent<Image> ().enabled = true;
		for (int i = 0; i < info_panel.transform.childCount; i++) {
			info_panel.transform.GetChild(i).gameObject.SetActive(true);
		}
		info_panel.GetComponent<InfoPanelScript>().defense = defense;	
	}

	public Vector2 TilePosition() {
		return transform.position;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Obstacle") {
			isOccupied = true;
		}
	}
	
	bool checkAIPath(){
		GetComponent<Node>().type = Node.NodeType.NODE_TOWER;
		List<GameObject> path = new List<GameObject>();
		path = GameObject.Find("Pathfinder").GetComponent<PathfinderScript>().FindPath(GameObject.Find("enemySpawn5").transform.position, Enemy.enemyType.TYPE_NORMAL);
		GetComponent<Node>().type = Node.NodeType.NODE_OPEN;
		
		if(path.Count > 0)
			//Normal monsters are still able to reach the core
			return true;
		else
			return false;
	}
}
