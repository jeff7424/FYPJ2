using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {

	public Defense defense;
	private int damage;
	private int speed;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		damage = defense.GetComponent<Defense> ().GetDamage ();
		speed = 1;
		direction = defense.GetComponent<Defense> ().GetDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		//check if enemy in range
		transform.position += direction * speed * Time.deltaTime;
	}

	// Check for collision with enemy
	// If collide, destroy this object
}
