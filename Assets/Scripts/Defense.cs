using UnityEngine;
using System.Collections;

public class Defense : MonoBehaviour {
	//declaration
	private int damage;
	private int cost;
	private int firerate;
	private int level;
	private Vector2 direction;
	public GameObject enemy;

	// Use this for initialization
	void Start () {
		//init
		damage = 10;
		level = 1;
		cost = 300;
		firerate = 3;
		direction = new Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		direction = enemy.transform.position - transform.position;
	}

	public Vector2 GetDirection() {
		return direction;
	}

	public int GetDamage() {
		return damage;
	}
}
