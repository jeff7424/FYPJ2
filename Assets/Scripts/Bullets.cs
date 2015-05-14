using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {

	public Defense defense;
	private int damage;
	private int speed;
	private Vector3 direction;
	public GameObject enemy;

	// Use this for initialization
	void Start () {
		speed = 1;
		damage = defense.GetComponent<Defense> ().GetDamage ();
		direction = enemy.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//check if enemy in range
		transform.position += direction * speed * Time.deltaTime;
	}

	// Check for collision with enemy
	// If collide, destroy this object
}
