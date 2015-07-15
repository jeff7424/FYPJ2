using UnityEngine;
using System.Collections;

public class DefenseMuzzleFlash : MonoBehaviour {

	float displayTime = 2.0f;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		FireMuzzle ();
	}

	void FireMuzzle() {
		GetComponent<SpriteRenderer>().enabled = true;
		displayTime -= Time.deltaTime;
		if (displayTime <= 0.0f) {
			GetComponent<SpriteRenderer>().enabled = false;
			Destroy (gameObject);
		}
	}	
}
