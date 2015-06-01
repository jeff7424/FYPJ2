using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Core : MonoBehaviour {

	public int health;
	public GameObject node;
	public GameObject thePathfinderRoot;
	public GameObject thePathfinder;
	public GameObject gameOver;

	// Use this for initialization
	void Start () {
		health = 10;
		gameOver = GameObject.Find ("GameOver");
		GameObject newNode = (GameObject)Instantiate(node, transform.position, Quaternion.identity);
		newNode.name = "enemyTargetPoint";
		newNode.transform.SetParent(thePathfinderRoot.transform);

		thePathfinder.GetComponent<AstarPath>().Scan();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			gameOver.GetComponent<Text>().enabled = true;
			Destroy (gameObject);
		} 
		else {
			gameOver.GetComponent<Text>().enabled= false;
		}
	}

	public int CurrentHealth() {
		return health;
	}

	public void DecreaseHealth() {
		if (CurrentHealth () >= 1)
			health -= 1;
		else
			Destroy (transform.parent.gameObject);
	}

	void  OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Enemy") {
			DecreaseHealth ();
			print ("Hit");
		}
	}
}
