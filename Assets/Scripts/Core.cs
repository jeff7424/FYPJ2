using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {

	int health;
	public GameObject node;
	public GameObject thePathfinderRoot;
	public GameObject thePathfinder;

	// Use this for initialization
	void Start () {
		health = 10;

		GameObject newNode = (GameObject)Instantiate(node, transform.position, Quaternion.identity);
		newNode.name = "enemyTargetPoint";
		newNode.transform.SetParent(thePathfinderRoot.transform);

		thePathfinder.GetComponent<AstarPath>().Scan();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public int CurrentHealth() {
		return health;
	}

	public void DecreaseHealth() {
		if (CurrentHealth () > 1)
			health -= 1;
		else
			Destroy (transform.parent.gameObject);
	}
}
