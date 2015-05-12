using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public GameObject defenseType;
	public bool isOccupied = false;
	public bool isHover = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void BuildDefense() {
		// Build tower when button is released
		//GameObject defense = (GameObject)Instantiate (defenseType);
		//defense.transform.position = transform.position;
		isOccupied = true;
		Debug.Log ("Defense built");
	}

	void OnMouseDown() {
		BuildDefense ();
		//Destroy (this.gameObject);
	}

	public Vector2 TilePosition() {
		return transform.position;
	}
}
