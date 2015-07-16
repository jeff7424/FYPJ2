using UnityEngine;
using System.Collections;

public class EnemyParentScript : MonoBehaviour {
	public PathfinderScript Pathfinder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SlowEnemies() {
		// Slow enemies on screen when pressed
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.GetComponent<Enemy>().SlowByBuff(3.0f, 0.3f);
		}
	}

	public void Kamikaze() {
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).gameObject.GetComponent<Enemy>().Kamikazed(15);
		}
	}
}
