using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {

	int health;

	// Use this for initialization
	void Start () {
		health = 10;
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
